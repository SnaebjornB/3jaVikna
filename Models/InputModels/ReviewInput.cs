using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class ReviewInput
    {
        public string review { get; set; }

        [Required(ErrorMessage = "Don't forget to rate the book")]
        [RegularExpression("[1-5]{1}", ErrorMessage = "Don't forget to rate the book")]
        public double rating { get; set; }
    }
}