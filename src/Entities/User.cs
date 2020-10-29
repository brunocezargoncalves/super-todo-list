using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Users")]    
    public class User : EntityBase<Guid>
    {
        [Required]
        [Column("Registration")]
        public DateTime Registration { get; set; }

        [Required]
        [Column("Name")]        
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "O e-mail deve ser inserido!")]        
        [Column("Email")]
        public string Email { get; set; }

        [Column("VerifiedEmail")]
        public bool VerifiedEmail { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "A senha deve ser inserida!")]        
        [Column("Password")]
        public string Password { get; set; }        

        [Column("ChangePassword")]
        public bool ChangePassword { get; set; }

        [Required]
        [Column("Role")]
        public string Role { get; set; }

        // [NotMapped]
        // public string Token {get; set; }
    }
}