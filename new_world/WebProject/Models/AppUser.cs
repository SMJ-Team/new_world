using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebProject.Models
{
    public class AppUser : IdentityUser
    {
        [Precision(12,2)] //9999999999,99
        public decimal Money { get; set; } = 0;
       
    }
}
