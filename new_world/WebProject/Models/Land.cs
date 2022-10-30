using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class Land
    {        
        public int Id { get; set; }
        public IdentityUser? User { get; set; }
        public bool IsOccupied { get; set; } = false;
        public int X { get; set; }
        public int Y { get; set; }
    }
}
