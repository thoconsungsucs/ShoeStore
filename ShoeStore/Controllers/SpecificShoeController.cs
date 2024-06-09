using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;
using ShoeStore.Models.ViewModel;
using ShoeStore.Ultility;

namespace ShoeStore.Controllers
{
    public class SpecificShoeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public SpecificShoeController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnviroment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Insert(int id)
        {
            var specificShoeVM = new SpecificShoeVM()
            {
                SpecificShoe = new SpecificShoe(),
                Shoe = _unitOfWork.Shoe.Get(s => s.ShoeId == id),
                GenderList = SD.GenderList,
                SizeList = SD.SizeList,
                ColorList = _unitOfWork.Color.GetAll().ToList().Select(i => new SelectListItem
                {
                    Text = i.ColorName,
                    Value = i.ColorId.ToString()
                }),
                DiscountList = _unitOfWork.Discount.GetAll().ToList().Select(i => new SelectListItem
                {
                    Text = i.DiscountName,
                    Value = i.DiscountId.ToString()
                })
            };
            return View(specificShoeVM);
        }

        [HttpPost]
        public IActionResult Insert(SpecificShoeVM specificShoeVM, List<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                TempData["Success"] = "Error while saving";
                return RedirectToAction(nameof(Index));
            }

            string wwwRootPath = _webHostEnviroment.WebRootPath;
            string color = _unitOfWork.Color.Get(s => s.ColorId == specificShoeVM.SpecificShoe.ColorId).ColorName;

            string filePath = Path.Combine("images", "shoes", color + " " + specificShoeVM.Shoe.ShoeName);
            string directoryPath = Path.Combine(wwwRootPath, filePath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            foreach (var file in files)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                using (var fileStream = new FileStream(Path.Combine(directoryPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                ImageShoe imageShoe = new ImageShoe
                {
                    ImageUrl = @"\" + filePath + @"\" + fileName,
                    ColorId = specificShoeVM.SpecificShoe.ColorId,
                    ShoeId = specificShoeVM.Shoe.ShoeId
                };
                _unitOfWork.ImageShoe.Add(imageShoe);
            }

            foreach (var size in specificShoeVM.SizeSelected)
            {
                SpecificShoe curShoe = new SpecificShoe
                {
                    ShoeId = specificShoeVM.Shoe.ShoeId,
                    Gender = specificShoeVM.SpecificShoe.Gender,
                    ColorId = specificShoeVM.SpecificShoe.ColorId,
                    Size = size,
                    DiscountId = specificShoeVM.SpecificShoe.DiscountId,
                    Price = specificShoeVM.SpecificShoe.Price,
                    Quantity = specificShoeVM.SpecificShoe.Quantity
                };
                _unitOfWork.SpecificShoe.Add(curShoe);
            }
            _unitOfWork.Save();
            TempData["Success"] = "Specific Shoe added successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
