using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebProject.Models;
using WebProject.Models.ViewModels;

namespace WebProject.Controllers
{
    [Authorize]
    public class MoneyController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public MoneyController(AppDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IActionResult Transaction()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Transaction(TransactionModel transaction)
        {
            string userId = transaction.userId;
            int money = transaction.money;
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var anotherUser = await _userManager.FindByIdAsync(userId);
            if (anotherUser == null)
                return View();
            if (currentUser.Money < money)
                return View();

            currentUser.Money -= money;
            anotherUser.Money += money;
            await _userManager.UpdateAsync(currentUser);
            await _userManager.UpdateAsync(anotherUser);
            return View();

        }
    }
}
