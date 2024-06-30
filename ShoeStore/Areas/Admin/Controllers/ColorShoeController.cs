using Microsoft.AspNetCore.Mvc;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models.ViewModel;
namespace ShoeStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorShoeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ColorShoeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
        public IActionResult GetColorShoeImages(int colorShoeId)
        {
            var colorShoe = _unitOfWork.ColorShoe.Get(cs => cs.ColorShoeId == colorShoeId, includeProperties: "Images");
            return PartialView("_ColorShoeImagesPartials", colorShoe);
        }
        #endregion
    }
}
