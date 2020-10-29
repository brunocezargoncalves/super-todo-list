using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "O e-mail deve ser inserido!")]        
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "A senha deve ser inserida!")]        
        public string Password { get; set; }   
        
    }
}