using System;
using System.Collections.Generic;
using System.Linq;
using BookCave.Data;
using BookCave.Models.ViewModels;

namespace BookCave.Repositories
{
    public class BookRepo
    {
        
        private DataContext _db;

        public BookRepo()
        {
            _db = new DataContext();
        }

        public List<BookView> GetSearchResultFromDB(string searchTitle, string searchAuthor, string searchISBN, string searchCategory, string orderBy)
        {
            if(orderBy == "ascendingPrice")
            {
                var searchResult = (from b in _db.Books
                                    where (String.IsNullOrEmpty(searchTitle) || b.title.ToLower().Contains(searchTitle.ToLower())) 
                                            && (String.IsNullOrEmpty(searchAuthor) || b.author.ToLower().Contains(searchAuthor.ToLower()))
                                            && (String.IsNullOrEmpty(searchISBN) || b.ISBN.ToLower().Contains(searchISBN.ToLower()))
                                            && (String.IsNullOrEmpty(searchCategory) || b.category.ToLower().Contains(searchCategory.ToLower()))
                                    orderby b.price ascending
                                    select new BookView{
                                        ID = b.ID,
                                        author = b.author,
                                        ISBN = b.ISBN,
                                        title = b.title,
                                        year = b.year,
                                        numberOfPages = b.numberOfPages,
                                        rating = b.rating,
                                        description = b.description,
                                        country = b.country,
                                        language = b.language,
                                        publisher = b.publisher,
                                        price = b.price,
                                        category = b.category,
                                        noOfSoldUnits = b.noOfSoldUnits,
                                        noOfCopiesAvailable = b.noOfCopiesAvailable,
                                        discount = b.discount
                                    }).ToList();
                return searchResult;
            }
            else if(orderBy == "descendingPrice")
            {
                var searchResult = (from b in _db.Books
                                    where (String.IsNullOrEmpty(searchTitle) || b.title.ToLower().Contains(searchTitle.ToLower())) 
                                            && (String.IsNullOrEmpty(searchAuthor) || b.author.ToLower().Contains(searchAuthor.ToLower()))
                                            && (String.IsNullOrEmpty(searchISBN) || b.ISBN.ToLower().Contains(searchISBN.ToLower()))
                                            && (String.IsNullOrEmpty(searchCategory) || b.category.ToLower().Contains(searchCategory.ToLower()))
                                    orderby b.price descending
                                    select new BookView{
                                        ID = b.ID,
                                        author = b.author,
                                        ISBN = b.ISBN,
                                        title = b.title,
                                        year = b.year,
                                        numberOfPages = b.numberOfPages,
                                        rating = b.rating,
                                        description = b.description,
                                        country = b.country,
                                        language = b.language,
                                        publisher = b.publisher,
                                        price = b.price,
                                        category = b.category,
                                        noOfSoldUnits = b.noOfSoldUnits,
                                        noOfCopiesAvailable = b.noOfCopiesAvailable,
                                        discount = b.discount
                                    }).ToList();
                return searchResult;
            }
            else if(orderBy == "descendingTitle")
            {
                var searchResult = (from b in _db.Books
                                    where (String.IsNullOrEmpty(searchTitle) || b.title.ToLower().Contains(searchTitle.ToLower())) 
                                            && (String.IsNullOrEmpty(searchAuthor) || b.author.ToLower().Contains(searchAuthor.ToLower()))
                                            && (String.IsNullOrEmpty(searchISBN) || b.ISBN.ToLower().Contains(searchISBN.ToLower()))
                                            && (String.IsNullOrEmpty(searchCategory) || b.category.ToLower().Contains(searchCategory.ToLower()))
                                    orderby b.title descending
                                    select new BookView{
                                        ID = b.ID,
                                        author = b.author,
                                        ISBN = b.ISBN,
                                        title = b.title,
                                        year = b.year,
                                        numberOfPages = b.numberOfPages,
                                        rating = b.rating,
                                        description = b.description,
                                        country = b.country,
                                        language = b.language,
                                        publisher = b.publisher,
                                        price = b.price,
                                        category = b.category,
                                        noOfSoldUnits = b.noOfSoldUnits,
                                        noOfCopiesAvailable = b.noOfCopiesAvailable,
                                        discount = b.discount
                                    }).ToList();
            
                return searchResult;
            }
            else
            {
                var searchResult = (from b in _db.Books
                                    where (String.IsNullOrEmpty(searchTitle) || b.title.ToLower().Contains(searchTitle.ToLower())) 
                                            && (String.IsNullOrEmpty(searchAuthor) || b.author.ToLower().Contains(searchAuthor.ToLower()))
                                            && (String.IsNullOrEmpty(searchISBN) || b.ISBN.ToLower().Contains(searchISBN.ToLower()))
                                            && (String.IsNullOrEmpty(searchCategory) || b.category.ToLower().Contains(searchCategory.ToLower()))
                                    orderby b.title ascending
                                    select new BookView{
                                        ID = b.ID,
                                        author = b.author,
                                        ISBN = b.ISBN,
                                        title = b.title,
                                        year = b.year,
                                        numberOfPages = b.numberOfPages,
                                        rating = b.rating,
                                        description = b.description,
                                        country = b.country,
                                        language = b.language,
                                        publisher = b.publisher,
                                        price = b.price,
                                        category = b.category,
                                        noOfSoldUnits = b.noOfSoldUnits,
                                        noOfCopiesAvailable = b.noOfCopiesAvailable,
                                        discount = b.discount
                                    }).ToList();
            
                return searchResult;
            }
            
            //Virkar ekki!
            //searchResult.OrderByDescending(b=>b.title);
            
            
        }

        public List<BookView> GetTop10BooksFromDB()
        {
           var top10 = (from b in _db.Books
                        orderby b.rating descending
                        select new BookView{
                            ID = b.ID,
                            author = b.author,
                            ISBN = b.ISBN,
                            title = b.title,
                            year = b.year,
                            numberOfPages = b.numberOfPages,
                            rating = b.rating,
                            description = b.description,
                            country = b.country,
                            language = b.language,
                            publisher = b.publisher,
                            price = b.price,
                            category = b.category,
                            noOfSoldUnits = b.noOfSoldUnits,
                            noOfCopiesAvailable = b.noOfCopiesAvailable,
                            discount = b.discount
                        }).Take(10).ToList();
           return top10;
        }

        public List<BookView> GetAllDiscountedBooks(){
            var discountedBooks = (from b in _db.Books
                                    where b.discount != 1 
                                    select new BookView{
                                        ID = b.ID,
                                        author = b.author,
                                        ISBN = b.ISBN,
                                        title = b.title,
                                        year = b.year,
                                        numberOfPages = b.numberOfPages,
                                        rating = b.rating,
                                        description = b.description,
                                        country = b.country,
                                        language = b.language,
                                        publisher = b.publisher,
                                        price = b.price,
                                        category = b.category,
                                        noOfSoldUnits = b.noOfSoldUnits,
                                        noOfCopiesAvailable = b.noOfCopiesAvailable,
                                        discount = b.discount
                                    }).ToList();
            return discountedBooks;
        }
    }
}