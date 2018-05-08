using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.ViewModels
{
    public class CCardInfoViewModel
    {
        [Required]
        public int lastDigits { get; set; }
        [Required]
        public int month { get; set; }
        [Required]
        public int year { get; set; }
    }
}