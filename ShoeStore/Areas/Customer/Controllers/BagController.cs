using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;
using ShoeStore.Ultility;
using System.Security.Claims;
namespace ShoeStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class BagController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BagController(IUnitOfWork unitOfWork)
        {
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

        #region API CALLS
        [HttpPost]
        public IActionResult AddToBag(int specificShoeId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var specificShoe = _unitOfWork.SpecificShoe.Get(ss => ss.SpecificShoeId == specificShoeId);

            var bag = _unitOfWork.Bag.Get(b => b.ApplicationUserId == userId && b.SpecificShoeId == specificShoeId);
            if (bag != null)
            {
                bag.Count += 1;
                _unitOfWork.Bag.Update(bag);
            }
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
