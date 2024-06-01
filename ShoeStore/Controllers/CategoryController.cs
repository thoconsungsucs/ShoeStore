using Microsoft.AspNetCore.Mvc;

namespace ShoeStore.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
