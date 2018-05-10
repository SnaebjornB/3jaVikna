using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCave.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter a valid email address.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your first name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your last name.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Enter a valid password.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm your password.")]
        [NotMapped]
        public string confirmPassword { get; set; }
    }
}