using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebProject.Extensions;
using WebProject.Models;

namespace WebProject.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private AppDbContext _dbContext;
        private UserManager<AppUser> _userManager;
        public AdminController(AppDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        private bool IsAdmin()
        {
            if (User.GetUserId() != "99f239e5-22c8-4a47-bc60-e78cbdc5c1a8")
                return false;
            return true;
        }
        public IActionResult Home()
        {
            if(!IsAdmin())            
                return StatusCode(404);
            var landList = _dbContext.Lands.Where(x => x.IsOccupied).ToList();
            return View(landList);
        }

        [HttpPost]
        public async Task<IActionResult> MakeMoney(decimal amount)
        {
            var landList = _dbContext.Lands.Where(x => x.IsOccupied).ToList();
            foreach(var item in landList)
            {
                var user = await _userManager.FindByIdAsync(item.UserId);
                user.Money += amount;
                _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Home");
        }
    }
}
