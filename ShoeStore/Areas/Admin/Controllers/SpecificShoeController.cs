﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;
using ShoeStore.Models.ViewModel;
using ShoeStore.Ultility;

namespace ShoeStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Employee")]
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
            // Check exist color shoe
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

                //
                string wwwRootPath = _webHostEnviroment.WebRootPath;
                // Get color name to create folder
                string color = _unitOfWork.Color.Get(s => s.ColorId == specificShoeVM.SpecificShoe.ColorShoe.ColorId).ColorName;

                string filePath = Path.Combine("images", "shoes", color + "_" + specificShoeVM.Shoe.ShoeName.Replace(' ', '_'));
                string directoryPath = Path.Combine(wwwRootPath, filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                bool isMain = true; // First img is main img
                if (_unitOfWork.ShoeImage.Get(si => si.ColorShoeId == colorShoe.ColorShoeId && si.IsMain) != null)
                {
                    isMain = false;
                }
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
            }

            foreach (var size in specificShoeVM.SizeSelected)
            {
                //Check exist specific shoe
                var specificShoe = _unitOfWork.SpecificShoe.Get(
                        s => s.ColorShoeId == colorShoe.ColorShoeId &&
                        s.Size == size &&
                        s.Gender == specificShoeVM.SpecificShoe.Gender
                    );
                if (specificShoe != null)
                {
                    TempData["Success"] += $"Specific Shoe already exist \nColorShoeId: {colorShoe.ColorShoeId}, Gender: {specificShoeVM.SpecificShoe.Gender}, Size: {size} \n\n";
                }
                else
                {
                    //Add new specific shoe
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
                    TempData["Success"] += $"Specific Shoe added successfully \nColorShoeId: {curShoe.ColorShoeId}, Gender: {curShoe.Gender}, Size: {curShoe.Size} \n\n";
                }
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int colorShoeId, int shoeId, Gender gender)
        {
            var specificShoeDetailVM = new SpecificShoeDetailsVM
            {
                ColorShoeIdWithImage = _unitOfWork.ColorShoe.GetColorShoeIdWithImage(shoeId, gender),
                SpecificShoeListForSize = _unitOfWork.SpecificShoe.GetSpecificShoeListForSize(colorShoeId, gender),
                ColorShoe = _unitOfWork.ColorShoe.Get(cs => cs.ColorShoeId == colorShoeId, includeProperties: "Shoe"),
                GenderList = SD.GenderList,
                DiscountList = _unitOfWork.Discount.GetAll().Select(i => new SelectListItem
                {
                    Text = i.DiscountName,
                    Value = i.DiscountId.ToString()
                })
            };

            specificShoeDetailVM.ColorShoe.Images = _unitOfWork.ShoeImage.GetAll(si => si.ColorShoeId == colorShoeId && !si.IsMain).ToList();
            if (specificShoeDetailVM.ColorShoe.Images.Count == 0)
            {
                specificShoeDetailVM.ColorShoe.Images.Add(new ShoeImage
                {
                    ImageUrl = "https://thumbs.dreamstime.com/b/light-abstract-empty-square-transparent-background-pattern-vector-231364928.jpg"
                });
            }
            return View(specificShoeDetailVM);
        }

        [HttpPost, ActionName("Details")]
        public IActionResult Edit(SpecificShoeDetailsVM specificShoeDetailsVM)
        {
            _unitOfWork.SpecificShoe.Update(specificShoeDetailsVM.SpecificShoe);
            _unitOfWork.Save();
            return RedirectToAction("Details", new
            {
                specificShoeDetailsVM.SpecificShoe.ColorShoeId,
                specificShoeDetailsVM.ColorShoe.Shoe.ShoeId,
                specificShoeDetailsVM.SpecificShoe.Gender
            }
            );
        }

        public IActionResult Delete(int specificShoeId)
        {
            var specificShoe = _unitOfWork.SpecificShoe.Get(s => s.SpecificShoeId == specificShoeId, includeProperties: "ColorShoe");
            _unitOfWork.SpecificShoe.Remove(specificShoe);
            _unitOfWork.Save();
            return RedirectToAction("Details", new
            {
                specificShoe.ColorShoeId,
                specificShoe.ColorShoe.ShoeId,
                specificShoe.Gender
            }
            );
        }

        public IActionResult DeleteAll(int colorShoeId, Gender gender)
        {
            var specificShoeList = _unitOfWork.SpecificShoe.GetAll(ss => ss.ColorShoeId == colorShoeId && ss.Gender == gender);
            foreach (var specificShoe in specificShoeList)
            {
                _unitOfWork.SpecificShoe.Remove(specificShoe);
            }
            _unitOfWork.Save();
            if (_unitOfWork.SpecificShoe.GetAll(ss => ss.ColorShoeId == colorShoeId).Count() == 0)
            {
                var colorShoe = _unitOfWork.ColorShoe.Get(cs => cs.ColorShoeId == colorShoeId, "Shoe");
                var shoeImages = _unitOfWork.ShoeImage.GetAll(si => si.ColorShoeId == colorShoeId);
                _unitOfWork.ColorShoe.Remove(colorShoe);
                foreach (var shoeImage in shoeImages)
                {
                    _unitOfWork.ShoeImage.Remove(shoeImage);
                }
                string wwwRootPath = _webHostEnviroment.WebRootPath;
                string color = _unitOfWork.Color.Get(c => c.ColorId == colorShoe.ColorId).ColorName;
                string filePath = Path.Combine("images", "shoes", color + "_" + colorShoe.Shoe.ShoeName.Replace(' ', '_'));
                string directoryPath = Path.Combine(wwwRootPath, filePath);
                if (Directory.Exists(directoryPath))
                {
                    Directory.Delete(directoryPath, true);
                }
                _unitOfWork.Save();
            }
            return RedirectToAction("Index");
        }

        [AllowAnonymous]  // Allow everyone to access this action
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
