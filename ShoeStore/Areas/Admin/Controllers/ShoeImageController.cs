using Microsoft.AspNetCore.Mvc;
using ShoeStore.DataAccess.Repository.IRepository;
namespace ShoeStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShoeImageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoeImageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region API CALLS
        [HttpPost]
        public IActionResult SetMainImage(bool isMain, int shoeImageId)
        {
            var shoeImage = _unitOfWork.ShoeImage.Get(si => si.ShoeImageId == shoeImageId);
            shoeImage.IsMain = isMain;
            _unitOfWork.ShoeImage.Update(shoeImage);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpPost]
        public IActionResult Delete(int shoeImageId)
        {
            var shoeImage = _unitOfWork.ShoeImage.Get(si => si.ShoeImageId == shoeImageId);
            _unitOfWork.ShoeImage.Remove(shoeImage);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}
