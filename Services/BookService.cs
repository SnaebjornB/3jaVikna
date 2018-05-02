using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class BookServices
    {
        private BookRepo _bookRepo;
        
        public BookServices()
        {
            _bookRepo = new BookRepo();
        }
        public List<BookView> GetTop10HighestRated()
        {
            var top10Books = _bookRepo.GetTop10BooksFromDB();
            return top10Books;
        }
    }
}