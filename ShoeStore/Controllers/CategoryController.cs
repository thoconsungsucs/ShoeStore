using Microsoft.AspNetCore.Mvc;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;

namespace ShoeStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> categoryList = _unitOfWork.Category.GetAll().ToList();
            return View(categoryList);
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
            var category = _unitOfWork.Category.Get(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Upsert(Category category)
        {
            //Add new category
            if (category.CategoryId == null || category.CategoryId == 0)
            {
                _unitOfWork.Category.Add(category);
                category.DateUpdated = DateOnly.FromDateTime(DateTime.UtcNow);
                TempData["Success"] = "Category added successfully";
            }
            else
            {
                //Update existing category
                category.DateUpdated = DateOnly.FromDateTime(DateTime.UtcNow);
                _unitOfWork.Category.Update(category);
                TempData["Success"] = "Category updated successfully";
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var category = _unitOfWork.Category.Get(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["Success"] = "Category deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
