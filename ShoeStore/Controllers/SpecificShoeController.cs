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


            var specificShoeList = new SpecificShoeListVM
            {
                SpecificShoeList = _unitOfWork.SpecificShoe.GetSpecificShoeWithImage(),
                ColorList = _unitOfWork.Color.GetAll().Select(c => new SelectListItem
                {
                    Text = c.ColorName,
                    Value = c.ColorId.ToString()
                }).ToList(),
                CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.CategoryId.ToString()
                }).ToList()
            };
            return View(specificShoeList);
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
            var colorShoe = _unitOfWork.ColorShoe.Get(cs => cs.ColorId == specificShoeVM.SpecificShoe.ColorShoe.ColorId
                                                      && cs.ShoeId == specificShoeVM.Shoe.ShoeId);
            if (colorShoe == null)
            {
                // Add new color shoe
                colorShoe = new ColorShoe
                {
                    ColorId = specificShoeVM.SpecificShoe.ColorShoe.ColorId,
                    ShoeId = specificShoeVM.Shoe.ShoeId
                };
                _unitOfWork.ColorShoe.Add(colorShoe);
                _unitOfWork.Save();
            }

            string wwwRootPath = _webHostEnviroment.WebRootPath;
            // Get color name to create folder
            string color = _unitOfWork.Color.Get(s => s.ColorId == specificShoeVM.SpecificShoe.ColorShoe.ColorId).ColorName;

            string filePath = Path.Combine("images", "shoes", color + " " + specificShoeVM.Shoe.ShoeName.Replace(' ', '_'));
            string directoryPath = Path.Combine(wwwRootPath, filePath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            bool isMain = true; // First img is main img
            foreach (var file in files)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                using (var fileStream = new FileStream(Path.Combine(directoryPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                ShoeImage imageShoe = new ShoeImage
                {
                    ImageUrl = @"\" + filePath + @"\" + fileName,
                    ColorShoeId = colorShoe.ColorShoeId,
                    IsMain = isMain
                };
                _unitOfWork.ShoeImage.Add(imageShoe);
                isMain = false;
            }

            foreach (var size in specificShoeVM.SizeSelected)
            {
                SpecificShoe curShoe = new SpecificShoe
                {
                    ColorShoeId = colorShoe.ColorShoeId,
                    Gender = specificShoeVM.SpecificShoe.Gender,
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

        public IActionResult Details(int colorShoeId, int shoeId, Gender gender)
        {
            var specificShoeDetailVM = new SpecificShoeDetailsVM
            {
                ColorShoeIdWithImage = _unitOfWork.ColorShoe.GetColorShoeIdWithImage(shoeId, gender),
                SpecificShoeListForSize = _unitOfWork.SpecificShoe.GetSpecificShoeListForSize(colorShoeId, gender),
                ColorShoe = _unitOfWork.ColorShoe.Get(cs => cs.ColorShoeId == colorShoeId, includeProperties: "Shoe")
            };

            specificShoeDetailVM.ColorShoe.Images = _unitOfWork.ShoeImage.GetAll(si => si.ColorShoeId == colorShoeId && !si.IsMain).ToList();
            return View(specificShoeDetailVM);
        }

        [HttpPost]
        public IActionResult Details(int specificShoeId)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Filter(List<int>? categories = null, List<Gender>? genders = null, List<string>? prices = null, List<int>? sizes = null, List<int>? colors = null)
        {
            var list = _unitOfWork.SpecificShoe
                .GetSpecificShoeWithImage(
                    categories,
                    genders,
                    prices,
                    sizes,
                    colors
                );
            return PartialView("_SpecificShoeListPartial", list);
        }


    }
}
