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
            /*var specificShoeList = _unitOfWork.SpecificShoe.GetAll(includeProperties: "Shoe,Discount")
                                    .GroupBy(s => new { s.Shoe.ShoeName, s.Gender, s.ShoeId })
                                    .Select(table => new
                                    {
                                        ShoeName = table.Key.ShoeName,
                                        Gender = table.Key.Gender,
                                        TotalColor = table.Select(s => s.ColorId).Distinct().Count(),
                                        DiscountMax = table.Select(s => s.Discount.DiscountValue).Max(),
                                        ShoeId = table.Key.ShoeId,
                                        FirstColorId = table.Select(s => s.ColorId).FirstOrDefault(),
                                    }).Join(_unitOfWork.ImageShoe,
                                            ImageShoe => new { ImageShoe.ShoeId, ImageShoe.ColorId },
                                            SpecificShoe => new { SpecificShoe.ShoeId, SpecificShoe.FirstColorId },
                                            (ImageShoe, SpecificShoe) => new
                                            {
                                                SpecificShoe.ShoeId = ImageShoe.ShoeId,
                                                SpecificShoe.FirstColorId = ImageShoe.ColorId,
                                            }
                                    );
            var shoeId = specificShoeList.Select(s => new
            {
                ShoeId = s.ShoeId,
                FirstColorId = s.FirstColorId
            }).ToList();*/

            /*var specificShoeList = _unitOfWork.SpecificShoe.GetAll(includeProperties: "Shoe,Discount")
                        .GroupBy(s => new { s.Shoe.ShoeName, s.Gender, s.ShoeId })
                        .Select(group => new
                        {
                            ShoeName = group.Key.ShoeName,
                            Gender = group.Key.Gender,
                            TotalColor = group.Select(s => s.ColorId).Distinct().Count(),
                            DiscountMax = group.Select(s => s.Discount.DiscountValue).Max(),
                            ShoeId = group.Key.ShoeId,
                            ColorId = group.Select(s => s.ColorId).FirstOrDefault(),

                        })
                        .Join(_unitOfWork.ImageShoe.GetAll(),
                              specificShoe => new { specificShoe.ShoeId, specificShoe.ColorId },
                              imageShoe => new { imageShoe.ShoeId, imageShoe.ColorId },
                              (specificShoe, imageShoe) => new
                              {
                                  specificShoe.ShoeName,
                                  specificShoe.Gender,
                                  specificShoe.TotalColor,
                                  specificShoe.DiscountMax,
                                  specificShoe.ShoeId,
                                  specificShoe.ColorId,
                                  ImageShoePath = imageShoe.ImageUrl
                              }).ToList();*/


            // Get all specific shoes with number of color, max discount and image url each color
            /*          var specificShoeListVM = _unitOfWork.SpecificShoe.GetAllGroupByShoeAndGender();*/

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

            string filePath = Path.Combine("images", "shoes", color + " " + specificShoeVM.Shoe.ShoeName);
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
    }
}
