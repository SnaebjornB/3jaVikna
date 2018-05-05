using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class ReviewInput
    {
        [Required(ErrorMessage = "Please write a review")]
        public string review { get; set; }

        [Required]
        public double rating { get; set; }
    }
}