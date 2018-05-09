using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.ViewModels
{
    public class CCardInfoViewModel
    {
        public int ID { get; set; }
        [Required]
        public string number { get; set; }
        [Required]
        public int month { get; set; }
        [Required]
        public int year { get; set; }
    }
}