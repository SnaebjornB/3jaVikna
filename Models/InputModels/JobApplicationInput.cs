using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class JobApplicationInput
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Don't forget to tell us what position you're applying for")]
        public string position { get; set; }

        [Required(ErrorMessage = "Please let us know when you can start")]
        public string startDate { get; set; }

        [Required(ErrorMessage = "Please let us know what your current employment status is")]
        public string employmentStatus { get; set; }

        public string resumeURL {get; set; }
    }
}