using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class BookInput{
        [Required]
        public string author { get; set; }
        [Required]
        public int ISBN { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public int year { get; set; }
        [Required]
        public int numberOfPages { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string country { get; set; }
        [Required]
        public string language { get; set; }
        [Required]
        public string publisher { get; set; }
        [Required]
        public double price { get; set; }
        [Required]
        public string category { get; set; }
        [Required]
        public int noOfCopiesAvailable { get; set; }
        [Required]
        public string image { get; set; }
    }
}
