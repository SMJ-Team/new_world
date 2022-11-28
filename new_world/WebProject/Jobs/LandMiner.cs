using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebProject.Models;

namespace WebProject.Jobs
{
    public class LandMiner : ILandMiner
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _signInManager;

        public LandMiner(AppDbContext appDbContext, UserManager<AppUser> signInManager)
        {
            _dbContext = appDbContext;
            _signInManager = signInManager;
        }
        public async Task MineMoneyAsync()
        {
            AppUser user;
            foreach (var item in _dbContext.Lands)
            {
                if(item.IsOccupied)
                {
                    user = await _signInManager.FindByIdAsync(item.UserId);
                    if (user.Money < 999999999) //999.999.999
                        user.Money += 1;
                    await _signInManager.UpdateAsync(user);
                }
            }
            
            
        }
    }
}
