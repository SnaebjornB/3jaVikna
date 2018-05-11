using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class JobApplicationEntity
    {
        public int ID { get; set;}
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string position { get; set; }
        public string startDate { get; set; }
        public string employmentStatus { get; set; }
        public string resumeURL {get; set; }
    }
}