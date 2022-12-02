using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Immutable;
using System.Security.Claims;
using WebProject.Extensions;
using WebProject.Models;
using WebProject.Models.ViewModels;

namespace WebProject.Controllers
{
    public class LandController : Controller
    {
        private readonly AppDbContext _dbContext;
        private UserManager<AppUser> _userManager { get; set; }
        const decimal LandPrice = 2500;

        public LandController(AppDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        
        //Вывод охбластей
        public IActionResult AreaList()
        {
            var areas = _dbContext.Areas.ToList();
            return View(areas);
        }

        //Вывод списка земли по id области
        public IActionResult LandList(int Areaid) 
        {
            if (_dbContext.Areas.FirstOrDefault(x => x.ID == Areaid) == null)
                return Redirect("/Land/AreaList/");
                
            var lands = _dbContext.Lands.Where(x => x.AreaId == Areaid).ToList();
            return View(lands);
        }

        //покупка земли по id земли
        public async Task<IActionResult> LandPurchase(int landId)
        {
            var user = await _userManager.FindByIdAsync(User.GetUserId());
            ViewBag.Money = user.Money;
            var Land = _dbContext.Lands.FirstOrDefault(x => x.ID == landId);
            if (Land == null)
            {
                return Redirect("Error");
            }
            ViewBag.LandId = Land.ID;
            ViewBag.AreaId = Land.AreaId;
            return View();
        }

        //Покупка земли по id земли
        [ActionName("LandPurchase")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LandPurchaseHandling(int landId)
        {
            
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var land = _dbContext.Lands.FirstOrDefault(x => x.ID == landId);

            //Проверка на наличие средств, существования земли и свободна ли она
            {
                if (user.Money < LandPrice) //2500 Tugrikov 
                {
                    ModelState.AddModelError("Money", "You don't have enough money for purchase!");
                    return View(landId);
                }
                if (land == null)
                {
                    ModelState.AddModelError("Land", $"Land with #{landId}-id doesn't exists!");
                    return View(landId);
                }
                if (land.IsOccupied == true)
                {
                    ModelState.AddModelError("Land", $"Land with #{landId}-id is occupied!");
                }
            }

            //Покупка земли - снятие денег со счета и назначение пользователя земли
            user.Money -= LandPrice;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return View(landId);
            land.IsOccupied = true;
            land.UserId = user.Id;
            _dbContext.SaveChanges();

            return Redirect($"/Land/LandList?AreaId={land.AreaId}"); 
           // AppUser user = _userManager.FindByIdAsync
        }

        [Authorize]
        public async Task<IActionResult> UsersLand()
        {
            var lands = _dbContext.Lands.Where(x => x.UserId == User.GetUserId());
            if (lands.IsNullOrEmpty())
                return View(null);
            return View(lands);
        }
    }
}
