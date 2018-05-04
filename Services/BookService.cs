using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;
using System.Linq;
using System;

namespace BookCave.Services
{
    public class BookServices
    {
        private BookRepo _bookRepo;
        
        public BookServices()
        {
            _bookRepo = new BookRepo();
        }

       public List<BookView> GetSearchResult(string searchTitle, string searchAuthor, string searchISBN, string searchCategory, string orderBy)
        {
            var searchResult = _bookRepo.GetSearchResultFromDB(searchTitle, searchAuthor, searchISBN, searchCategory, orderBy);
            return searchResult;
        }

        public List<BookView> GetTop10HighestRated()
        {
            var top10Books = _bookRepo.GetTop10BooksFromDB();
            return top10Books;
        }

        public List<BookView> GetAllDiscountedBooks()
        {
            var discountedBooks = _bookRepo.GetAllDiscountedBooks();
            discountedBooks = (from b in discountedBooks
                                orderby b.title
                                select b).ToList();
            return discountedBooks;
        }

        public BookView GetBookDetail(int? id)
        {
            var bookDetail = _bookRepo.GetBookDetail(id);
            return bookDetail;
        }
    }
}