using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class Area
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string? AreaType { get; set; }
    }
}
