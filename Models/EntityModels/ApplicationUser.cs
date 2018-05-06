using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BookCave.Models.EntityModels
{
    public class ApplicationUser : IdentityUser
    {
        public string image { get; set; }
        public string favBook { get; set; }
    }
}