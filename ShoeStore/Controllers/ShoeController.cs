using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;
using ShoeStore.Models.ViewModel;
namespace ShoeStore.Controllers
{
    public class ShoeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Shoe> ShoeList = _unitOfWork.Shoe.GetAll(includeProperties: "Category").ToList();
            return View(ShoeList);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ShoeVM shoeVM = new ShoeVM()
            {
                Shoe = new Shoe(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.CategoryName,
                    Value = i.CategoryId.ToString()
                })
            };

            if (id == null)
            {
                //Create
                return View(shoeVM);
            }
            //Edit
            shoeVM.Shoe = _unitOfWork.Shoe.Get(c => c.ShoeId == id);
            if (shoeVM.Shoe == null)
            {
                return NotFound();
            }
            return View(shoeVM);
        }

        [HttpPost]
        public IActionResult Upsert(Shoe Shoe)
        {
            //Add new Shoe
            if (Shoe.ShoeId == null || Shoe.ShoeId == 0)
            {
                _unitOfWork.Shoe.Add(Shoe);
                TempData["Success"] = "Shoe added successfully";
            }
            else
            {
                //Update existing Shoe
                _unitOfWork.Shoe.Update(Shoe);
                TempData["Success"] = "Shoe updated successfully";
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            ShoeVM shoeVM = new ShoeVM()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.CategoryName,
                    Value = i.CategoryId.ToString()
                }),
                Shoe = _unitOfWork.Shoe.Get(c => c.ShoeId == id)
            };
            if (shoeVM.Shoe == null)
            {
                return NotFound();
            }
            return View(shoeVM);
        }

        [HttpPost]
        public IActionResult Delete(ShoeVM shoeVM)
        {
            _unitOfWork.Shoe.Remove(shoeVM.Shoe);
            _unitOfWork.Save();
            TempData["Success"] = "Shoe deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var shoeList = _unitOfWork.Shoe.GetAll(includeProperties: "Category")
                                           .Select(s => new
                                           {
                                               Id = s.ShoeId,
                                               ShoeName = s.ShoeName,
                                               Price = s.Price,
                                               CategoryName = s.Category.CategoryName
                                           }).ToList();
            return Json(new { data = shoeList });
        }
        #endregion
    }
}
