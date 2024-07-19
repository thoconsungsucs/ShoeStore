﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.DataAccess.Repository.IRepository;
using ShoeStore.Models;
using System.Security.Claims;
namespace ShoeStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class BagController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BagController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API CALLS
        [HttpPost]
        public IActionResult AddToBag(int specificShoeId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var specificShoe = _unitOfWork.SpecificShoe.Get(ss => ss.SpecificShoeId == specificShoeId);

            var bag = _unitOfWork.Bag.Get(b => b.ApplicationUserId == userId && b.SpecificShoeId == specificShoeId);
            if (bag != null)
            {
                bag.Count += 1;
                _unitOfWork.Bag.Update(bag);
            }
            else
            {
                bag = new Bag
                {
                    ApplicationUserId = userId,
                    SpecificShoeId = specificShoeId,
                    Count = 1
                };
                _unitOfWork.Bag.Add(bag);
            }
            _unitOfWork.Save();
            return Json(new { success = true, message = "Shoe added to bag successfully" });
        }
        #endregion
    }
}