using System.ComponentModel.DataAnnotations;

namespace WebProject.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]        
        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]        
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

            
        [Display(Name = "Remember?")]        
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
