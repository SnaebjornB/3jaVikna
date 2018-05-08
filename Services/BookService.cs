using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;
using System.Linq;
using System;
using BookCave.Models.InputModels;
using Microsoft.AspNetCore.Authorization;

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
            
            if(orderBy == "ascendingPrice")
            {
                searchResult = (from b in searchResult
                                orderby b.price ascending
                                select b).ToList();
            }
            if(orderBy == "descendingPrice")
            {
                searchResult = (from b in searchResult
                                orderby b.price descending
                                select b).ToList();
            }
            if(orderBy == "descendingTitle")
            {
                searchResult = (from b in searchResult
                                orderby b.title descending
                                select b).ToList();
            }
            if(orderBy == "ascendingTitle")
            {
                searchResult = (from b in searchResult
                                orderby b.title ascending
                                select b).ToList();
            }

            return searchResult;
        }
        [Authorize(Roles="Employee")]
        internal void UpdateBook(BookInputModel book)
        {
            _bookRepo.UpdateBook(book);
        }

        [Authorize(Roles="Employee")]
        internal object GetBookToEdit(int id)
        {
            return _bookRepo.GetBookToEdit(id);
        }

        [Authorize(Roles="Employee")]
        internal void AddNewBook(BookInputModel book)
        {
            _bookRepo.AddNewBook(book);
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
        [Authorize(Roles="Employee")]
        internal void EditDiscount(List<BookView> books, double discount)
        {   
            if(discount >= 0 && discount <= 100)
            {
                discount = 1 - discount/100;
                _bookRepo.EditDiscount(books, discount);
            }
        }

        public bool IsBookInDatabase(int? id)
        {
            return _bookRepo.IsBookInDatabase(id);
        }

        public BookView GetBookDetail(int? id)
        {
            var bookDetail = _bookRepo.GetBookDetail(id);
            return bookDetail;
        }

        public void AddReview(int? id, ReviewInput newReview, string userID, string userName)
        {
            _bookRepo.AddReview(id, newReview, userID, userName);
        }
        public List<BookView> GetBooksByAuthor(string author){
            return _bookRepo.GetBooksByAuthor(author);
        }
    }
}