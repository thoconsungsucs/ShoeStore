using Microsoft.AspNetCore.Mvc;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;

namespace ShoeStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ColorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Color> ColorList = _unitOfWork.Color.GetAll().ToList();
            return View(ColorList);
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
            var Color = _unitOfWork.Color.Get(c => c.ColorId == id);
            if (Color == null)
            {
                return NotFound();
            }
            return View(Color);
        }

        [HttpPost]
        public IActionResult Upsert(Color Color)
        {
            //Add new Color
            if (Color.ColorId == 0)
            {
                _unitOfWork.Color.Add(Color);
                Color.DateUpdated = DateOnly.FromDateTime(DateTime.UtcNow);
                TempData["Success"] = "Color added successfully";
            }
            else
            {
                //Update existing Color
                Color.DateUpdated = DateOnly.FromDateTime(DateTime.UtcNow);
                _unitOfWork.Color.Update(Color);
                TempData["Success"] = "Color updated successfully";
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var Color = _unitOfWork.Color.Get(c => c.ColorId == id);
            if (Color == null)
            {
                return NotFound();
            }
            return View(Color);
        }

        [HttpPost]
        public IActionResult Delete(Color Color)
        {
            _unitOfWork.Color.Remove(Color);
            _unitOfWork.Save();
            TempData["Success"] = "Color deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
