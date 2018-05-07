using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.ViewModels
{
    public class EditUserViewModel
    {   
        [Required(ErrorMessage = "A first name is required")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "A last name is required")]
        public string lastName { get; set; }
        public string image { get; set; }
        public string favBook { get; set; }
    }
}