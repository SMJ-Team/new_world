using Microsoft.AspNetCore.Mvc;
using WebProject.Models;

namespace WebProject.Jobs
{
    public interface ILandMiner
    {        
        Task MineMoneyAsync();
    }
}
