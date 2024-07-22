using Microsoft.AspNetCore.Mvc;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Ultility;
using System.Security.Claims;
namespace ShoeStore.ViewComponents

{
    public class BagViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public BagViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                if (HttpContext.Session.GetInt32(SD.BagSession) == null)
                {
                    HttpContext.Session.SetInt32(SD.BagSession, _unitOfWork.Bag.GetAll(b => b.ApplicationUserId == claim.Value).Count());
                }
                return View(HttpContext.Session.GetInt32(SD.BagSession));
            }
            else
            {
                HttpContext.Session.Clear();
            }
            return View(0);
        }
    }
}
