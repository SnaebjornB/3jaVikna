using System.Collections.Generic;

namespace BookCave.Models.ViewModels
{
    public class AuthorDetailsViewModel
    {
        public string author { get; set; }
        public List<BookView> books { get; set; }
    }
}