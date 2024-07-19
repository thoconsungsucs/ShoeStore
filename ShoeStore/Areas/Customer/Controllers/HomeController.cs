using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;
using ShoeStore.Models.ViewModel;
using ShoeStore.Ultility;
using System.Diagnostics;

namespace ShoeStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
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

        public IActionResult Add(int specificShoeId)
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
