using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BookCave.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string image { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string favBook { get; set; }
    }
}