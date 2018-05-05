using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.ViewModels
{
    public class UserViewModel
    {
        [EmailAddress]
        public string email { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string address1 { get; set; }
        public string address2 {get; set; }
        public string favBook { get; set; }
    }
}