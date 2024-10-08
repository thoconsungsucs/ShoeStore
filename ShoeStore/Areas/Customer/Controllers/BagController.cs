﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;
using ShoeStore.Models.ViewModel;
using ShoeStore.Ultility;
using ShoeStore.Ultility.VnPay;
using System.Security.Claims;
using System.Text;
namespace ShoeStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class BagController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVnPayService _vnPayservice;
        public BagController(IUnitOfWork unitOfWork, IVnPayService vnPayservice)
        {
            _vnPayservice = vnPayservice;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var bagList = _unitOfWork.Bag.GetAll(userId);
            return View(bagList);
        }

        public IActionResult ChangeQuantity(int bagId, int quantity)
        {
            var bag = _unitOfWork.Bag.Get(b => b.BagId == bagId);
            var specificShoeQuantity = _unitOfWork.SpecificShoe.Get(ss => ss.SpecificShoeId == bag.SpecificShoeId).Quantity;
            if (quantity < 1 || quantity > specificShoeQuantity)
            {
                TempData["Error"] = "Quantity must be between 1 and " + specificShoeQuantity;
                return RedirectToAction(nameof(Index));
            }
            bag.Count = quantity;
            _unitOfWork.Bag.Update(bag);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Remove(int bagId)
        {
            var bag = _unitOfWork.Bag.Get(b => b.BagId == bagId);
            _unitOfWork.Bag.Remove(bag);
            _unitOfWork.Save();
            HttpContext.Session.SetInt32(SD.BagSession, _unitOfWork.Bag.GetAll(b => b.ApplicationUserId == bag.ApplicationUserId).Count());
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Checkout()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var bagVM = _unitOfWork.Bag.GetAll(userId);
            var applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

            // Add current user information to order header
            bagVM.OrderHeader.Name = applicationUser.Name;
            bagVM.OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
            bagVM.OrderHeader.StreetAddress = applicationUser.StreetAddress;
            bagVM.OrderHeader.City = applicationUser.City;
            bagVM.OrderHeader.District = applicationUser.District;

            if (bagVM.Bags.Count == 0)
            {
                TempData["Error"] = "Your bag is empty, please add a shoe to your bag";
                return RedirectToAction(nameof(Index));
            }

            return View(bagVM);
        }

        public IActionResult PlaceOrder(BagVM bagVM)
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            bagVM.Bags = _unitOfWork.Bag.GetAll(userId).Bags;
            bagVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            bagVM.OrderHeader.OrderStatus = SD.OrderStatusPending;
            bagVM.OrderHeader.OrderDate = DateTime.Now;
            bagVM.OrderHeader.ApplicationUserId = userId;
            bagVM.OrderHeader.PaymentMethod = SD.PaymentMethodVnPay;

            _unitOfWork.OrderHeader.Add(bagVM.OrderHeader);
            _unitOfWork.Save();

            // Order header description
            StringBuilder sb = new StringBuilder();
            sb.Append("Pay for shoes:");
            bagVM.Bags.ForEach(b =>
            {
                sb.Append($" {b.Key.SpecificShoe.ColorShoe.Shoe.ShoeName}-{b.Key.SpecificShoe.Size}-{b.Key.Count}, ");
            });
            var vnPayModel = new VnPaymentRequestModel
            {
                Amount = bagVM.OrderHeader.OrderTotal,
                CreatedDate = DateTime.Now,
                Description = sb.ToString(),
                OrderId = bagVM.OrderHeader.OrderHeaderId
            };
            // Redirect to payment page
            return Redirect(_vnPayservice.CreatePaymentUrl(HttpContext, vnPayModel));
        }

        // After user paid
        public IActionResult PaymentCallBack(int orderHeaderId)
        {
            var collections = HttpContext.Request.Query;
            var response = _vnPayservice.PaymentExecute(collections);
            var order = _unitOfWork.OrderHeader.Get(o => o.OrderHeaderId == orderHeaderId);
            order.TransactionId = response.TransactionId;
            order.PaymentDate = response.PaymentDate;
            // Success
            if (response.VnPayResponseCode == "00")
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                order.PaymentStatus = SD.PaymentStatusApproved;
                _unitOfWork.OrderHeader.Update(order);
                var bagVM = _unitOfWork.Bag.GetAll(userId);
                foreach (var bag in bagVM.Bags)
                {
                    if (bag.Value)
                    {
                        OrderDetail orderDetail = new OrderDetail
                        {
                            SpecificShoeId = bag.Key.SpecificShoeId,
                            OrderHeaderId = orderHeaderId,
                            Quantity = bag.Key.Count,
                            Price = (1 - bag.Key.SpecificShoe.Discount.DiscountValue) * bag.Key.SpecificShoe.Price
                        };
                        _unitOfWork.OrderDetail.Add(orderDetail);
                        var specificShoe = _unitOfWork.SpecificShoe.Get(ss => ss.SpecificShoeId == bag.Key.SpecificShoeId);
                        specificShoe.Quantity -= bag.Key.Count;
                        _unitOfWork.SpecificShoe.Update(specificShoe);
                    }
                }
                bagVM.Bags.ForEach(b =>
                {
                    if (b.Value)
                    {
                        b.Key.SpecificShoe = null;
                        _unitOfWork.Bag.Remove(b.Key);
                    }
                });
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.BagSession, 0);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Error
                order.PaymentStatus = SD.PaymentStatusRejected;
                _unitOfWork.OrderHeader.Update(order);
                _unitOfWork.Save();
            }
            return RedirectToAction("Index", "Bag");
        }

        #region API CALLS
        [HttpPost]
        public IActionResult AddToBag(int specificShoeId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var specificShoe = _unitOfWork.SpecificShoe.Get(ss => ss.SpecificShoeId == specificShoeId);

            var bag = _unitOfWork.Bag.Get(b => b.ApplicationUserId == userId && b.SpecificShoeId == specificShoeId);
            // Add 1 unit to shoe already in bag
            if (bag != null)
            {
                bag.Count += 1;
                _unitOfWork.Bag.Update(bag);
            }
            // If shoe is not in bag
            else
            {
                bag = new Bag
                {
                    ApplicationUserId = userId,
                    SpecificShoeId = specificShoeId,
                    Count = 1
                };
                _unitOfWork.Bag.Add(bag);
                HttpContext.Session.SetInt32(SD.BagSession, _unitOfWork.Bag.GetAll(b => b.ApplicationUserId == userId).Count() + 1);
            }
            _unitOfWork.Save();
            return Json(new { success = true, message = "Shoe added to bag successfully" });
        }
        #endregion
    }
}
