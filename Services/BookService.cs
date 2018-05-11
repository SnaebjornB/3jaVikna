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

       public List<BookView> GetSearchResult(string searchTitle, string searchAuthor, string searchISBN, string searchCategory, string orderBy, int searchYearFrom, int searchYearTo)
        {
            var searchResult = _bookRepo.GetSearchResultFromDB(searchTitle, searchAuthor, searchISBN, searchCategory, orderBy, searchYearFrom, searchYearTo);

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

            foreach (var book in searchResult)
            {
                book.price = Math.Round(book.price, 2);
            }

            return searchResult;
        }

        public List<BookView> GetSearchResultForBar(string searchWord)
        {
            var searchResult = _bookRepo.GetSearchResultFromDBForBar(searchWord);
            
            searchResult = (from b in searchResult
                            orderby b.title descending
                            select b).ToList();     

            foreach (var book in searchResult)
            {
                book.price = Math.Round(book.price, 2);
            }       

            return searchResult;
        }

        public List<BookView> GetAllBooks()
        {
            var allBooks = _bookRepo.GetAllBooks();

            foreach (var book in allBooks)
            {
                book.price = Math.Round(book.price, 2);
            }

            return allBooks;
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

            foreach (var book in top10Books)
            {
                book.price =  Math.Round(book.price, 2);
            }
            return top10Books;
        }

        public List<BookView> GetAllDiscountedBooks()
        {
            var discountedBooks = _bookRepo.GetAllDiscountedBooks();
            discountedBooks = (from b in discountedBooks
                                orderby b.title
                                select b).ToList();
            foreach (var book in discountedBooks)
            {
                book.price =  Math.Round(book.price, 2);
            }

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

            bookDetail.price = Math.Round(bookDetail.price,2);
            return bookDetail;
        }

        public void AddReview(int? id, ReviewInput newReview, string userID, string userName)
        {
            _bookRepo.AddReview(id, newReview, userID, userName);
        }

        internal void AddJobApplication(JobApplicationInput newApplication)
        {
            _bookRepo.AddJobApplication(newApplication);
        }
        public List<BookView> GetBooksByAuthor(string author){
            return _bookRepo.GetBooksByAuthor(author);
        }
    }
}