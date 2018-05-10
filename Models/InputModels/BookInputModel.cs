using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class BookInputModel{
        public int ID { get; set; }

        [Required(ErrorMessage="Input author name")]
        public string author { get; set; }

        [RegularExpression("[0-9]*[-| ][0-9]*[-| ][0-9]*[-| ][0-9]*[-| ][0-9]*", ErrorMessage="Input ISBN number (XXX-X-XX-XXXXXX-X)")]
        public string ISBN { get; set; }

        [Required(ErrorMessage="Input book title")]
        public string title { get; set; }

        [Required(ErrorMessage="Input short title")]
        [MaxLength(24)]
        public string shortTitle { get; set; }

        [Required(ErrorMessage="Input year")]
        public int year { get; set; }

        [Required(ErrorMessage="Input number of pages")]
        public int numberOfPages { get; set; }

        [Required(ErrorMessage="Input a description")]
        public string description { get; set; }

        [Required(ErrorMessage="Select a country")]
        public string country { get; set; }

        [Required(ErrorMessage="Input a language")]
        public string language { get; set; }

        [Required(ErrorMessage="Input publisher")]
        public string publisher { get; set; }

        [Required(ErrorMessage="Input price")]
        public double price { get; set; }

        [Required(ErrorMessage="select genre")]
        public string category { get; set; }

        [Required(ErrorMessage="Input the number of copies available")]
        public int noOfCopiesAvailable { get; set; }

        [Required(ErrorMessage="Input cover image URL")]
        public string image { get; set; }
    }
}
