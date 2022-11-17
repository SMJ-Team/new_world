using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class Land
    {
        [Key]
        public int ID { get; set; }
        public string? UserId { get; set; }
        public bool IsOccupied { get; set; } = false;
        public int AreaId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
