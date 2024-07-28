using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models.ViewModel;
using ShoeStore.Ultility;
using ShoeStore.Ultility.VnPay;
namespace ShoeStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Employee")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVnPayService _vnPayservice;
        public OrderController(IUnitOfWork unitOfWork, IVnPayService vnPayService)
        {
            _unitOfWork = unitOfWork;
            _vnPayservice = vnPayService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Update(OrderVM orderVM)
        {
            var orderFromDb = _unitOfWork.OrderHeader.Get(o => o.OrderHeaderId == orderVM.OrderHeader.OrderHeaderId);
            if (orderFromDb.OrderStatus == SD.OrderStatusPending)
            {
                orderFromDb.Name = orderVM.OrderHeader.Name;
                orderFromDb.PhoneNumber = orderVM.OrderHeader.PhoneNumber;
                orderFromDb.StreetAddress = orderVM.OrderHeader.StreetAddress;
                orderFromDb.City = orderVM.OrderHeader.City;
                orderFromDb.District = orderVM.OrderHeader.District;
                orderFromDb.TrackingNumber = orderVM.OrderHeader.TrackingNumber;
                _unitOfWork.OrderHeader.Update(orderFromDb);
                _unitOfWork.Save();
                TempData["Success"] = "Order updated successfully";
            }
            else
            {
                TempData["Error"] = "Order can't be updated";
            }
            return RedirectToAction("Details", new { orderHeaderId = orderVM.OrderHeader.OrderHeaderId });

        }

        public IActionResult Details(int orderHeaderId)
        {
            var orderVM = new OrderVM
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(o => o.OrderHeaderId == orderHeaderId),
                OrderDetails = _unitOfWork.OrderDetail.GetAllSpecificShoe(orderHeaderId)
            };
            return View(orderVM);
        }

        public IActionResult StartProcessing(int orderHeaderId)
        {
            var orderFromDb = _unitOfWork.OrderHeader.Get(o => o.OrderHeaderId == orderHeaderId);
            orderFromDb.OrderStatus = SD.OrderStatusInProcess;
            _unitOfWork.OrderHeader.Update(orderFromDb);
            _unitOfWork.Save();
            TempData["Success"] = "Order is in process";
            return RedirectToAction("Details", new { orderHeaderId = orderHeaderId });
        }

        public IActionResult Ship(int orderHeaderId)
        {
            var orderFromDb = _unitOfWork.OrderHeader.Get(o => o.OrderHeaderId == orderHeaderId);
            orderFromDb.OrderStatus = SD.OrderStatusShipped;
            orderFromDb.ShippingDate = DateTime.Now;
            _unitOfWork.OrderHeader.Update(orderFromDb);
            _unitOfWork.Save();
            TempData["Success"] = "Order shipped successfully";
            return RedirectToAction("Details", new { orderHeaderId = orderHeaderId });
        }

        public async Task<IActionResult> Cancel(int orderHeaderId)
        {
            var orderFromDb = _unitOfWork.OrderHeader.Get(o => o.OrderHeaderId == orderHeaderId);
            var requestModel = new VnPaymentRequestModel
            {
                OrderId = orderHeaderId,
                Amount = orderFromDb.OrderTotal,
                CreatedDate = DateTime.Now,
                Description = "Refund for order " + orderHeaderId,
                TransactionDate = (DateTime)orderFromDb.PaymentDate,
                Name = orderFromDb.Name
            };

            string jsonResponse = await _vnPayservice.SendRefundRequest(HttpContext, requestModel);

            var response = JsonConvert.DeserializeObject<RefundResponse>(jsonResponse);
            if (response.Vnp_ResponseCode == "00")
            {
                orderFromDb.OrderStatus = SD.OrderStatusCancelled;
                orderFromDb.PaymentStatus = SD.PaymentStatusRefund;
                _unitOfWork.OrderHeader.Update(orderFromDb);
                _unitOfWork.Save();
                TempData["Success"] = "Refund successful";
            }
            else
            {
                TempData["Error"] = "Refund failed";
            }
            return RedirectToAction("Details", new { orderHeaderId = orderHeaderId });
        }

        #region API CALLS
        public IActionResult GetAll()
        {
            var orderList = _unitOfWork.OrderHeader.GetAll();
            return Json(new { data = orderList });
        }
        #endregion
    }
}
