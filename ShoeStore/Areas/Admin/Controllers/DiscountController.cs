using Microsoft.AspNetCore.Mvc;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;

namespace ShoeStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DiscountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DiscountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Discount> DiscountList = _unitOfWork.Discount.GetAll().ToList();
            return View(DiscountList);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if (id == null)
            {
                //Create
                return View();
            }
            //Edit
            var Discount = _unitOfWork.Discount.Get(c => c.DiscountId == id);
            if (Discount == null)
            {
                return NotFound();
            }
            return View(Discount);
        }

        [HttpPost]
        public IActionResult Upsert(Discount Discount)
        {
            //Add new Discount
            if (Discount.DiscountId == 0)
            {
                _unitOfWork.Discount.Add(Discount);
                TempData["Success"] = "Discount added successfully";
            }
            else
            {
                //Update existing Discount
                _unitOfWork.Discount.Update(Discount);
                TempData["Success"] = "Discount updated successfully";
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var Discount = _unitOfWork.Discount.Get(c => c.DiscountId == id);
            if (Discount == null)
            {
                return NotFound();
            }
            return View(Discount);
        }

        [HttpPost]
        public IActionResult Delete(Discount Discount)
        {
            _unitOfWork.Discount.Remove(Discount);
            _unitOfWork.Save();
            TempData["Success"] = "Discount deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
