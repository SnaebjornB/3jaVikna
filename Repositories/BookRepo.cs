using System;
using System.Collections.Generic;
using System.Linq;
using BookCave.Data;
using BookCave.Models.EntityModels;
using BookCave.Models.InputModels;
using BookCave.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BookCave.Repositories
{
    public class BookRepo
    {
        
        private DataContext _db;
        private ReviewEntity reviewEntity;
        private BookEntity bookEntity;

        public BookRepo()
        {
            _db = new DataContext();
            reviewEntity = new ReviewEntity();
            bookEntity = new BookEntity();
        }

        public List<BookView> GetSearchResultFromDB(string searchTitle, string searchAuthor, string searchISBN, string searchCategory, string orderBy)
        {
            var searchResult = (from b in _db.Books
                                where (String.IsNullOrEmpty(searchTitle) || b.title.ToLower().Contains(searchTitle.ToLower())) 
                                        && (String.IsNullOrEmpty(searchAuthor) || b.author.ToLower().Contains(searchAuthor.ToLower()))
                                        && (String.IsNullOrEmpty(searchISBN) || b.ISBN.ToLower().Contains(searchISBN.ToLower()))
                                        && (String.IsNullOrEmpty(searchCategory) || b.category.ToLower().Contains(searchCategory.ToLower()))
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
                                    discount = b.discount,
                                    image = b.image
                                }).ToList();

            return searchResult;
        }
        [Authorize(Roles="Employee")]
        internal void UpdateBook(BookInputModel book)
        {
            var existing = _db.Books.SingleOrDefault(a => a.ID == book.ID);
            if(existing != null)
            {
                existing.ID = book.ID;
                existing.author = book.author;
                existing.ISBN = book.ISBN;
                existing.title = book.title;
                existing.year = book.year;
                existing.numberOfPages = book.numberOfPages;
                existing.description = book.description;
                existing.country = book.country;
                existing.language = book.language;
                existing.publisher = book.publisher;
                existing.price = book.price;
                existing.category = book.category;
                existing.noOfCopiesAvailable = book.noOfCopiesAvailable;
                existing.image = book.image;
                _db.SaveChanges();
            }
            else
            {
                throw new Exception("BookInputModel is empty");
            }
        }
        [Authorize(Roles="Employee")]
        internal void EditDiscount(List<BookView> books, double discount)
        {
            var dbBooks = new List<BookEntity>();
            foreach(var book in books)
            {
                var temp = (from b in _db.Books
                            where book.ID == b.ID
                            select b).FirstOrDefault();
                temp.discount = discount;
                dbBooks.Add(temp);
            }
            if(dbBooks != null)
            {
                _db.SaveChanges();
            }
        }
        [Authorize(Roles="Employee")]
        internal object GetBookToEdit(int id)
        {
            var book = (from b in _db.Books
                        where id == b.ID
                        select new BookInputModel{
                            ID = b.ID,
                            title = b.title,
                            author = b.author,
                            category = b.category,
                            country = b.country,
                            description = b.description,
                            noOfCopiesAvailable = b.noOfCopiesAvailable,
                            numberOfPages = b.numberOfPages,
                            ISBN = b.ISBN,
                            year = b.year,
                            language = b.language,
                            publisher = b.publisher,
                            price = b.price,
                            image = b.image
                        }).FirstOrDefault();
            return book;
        }

        [Authorize(Roles="Employee")]
        internal void AddNewBook(BookInputModel book)
        {
            var newBook = new BookEntity{
                            title = book.title,
                            author = book.author,
                            category = book.category,
                            country = book.country,
                            description = book.description,
                            noOfCopiesAvailable = book.noOfCopiesAvailable,
                            numberOfPages = book.numberOfPages,
                            ISBN = book.ISBN,
                            year = book.year,
                            language = book.language,
                            publisher = book.publisher,
                            price = book.price,
                            image = book.image,
                            rating = 0,
                            noOfRatings = 1,
                            noOfSoldUnits = 0,
                            discount = 1
                        };
            
            _db.Books.Add(newBook);
            _db.SaveChanges();
        }

        public bool IsBookInDatabase(int? id)
        {
            bool dbCheck = true;
            //Er bókin í database?

            try{
                var bookCheck = (from b in _db.Books
                                where b.ID == id.GetValueOrDefault()
                                select b).Single();
            }
            catch (Exception){
                dbCheck = false;
            }

            return dbCheck;
        }

        public void AddReview(int? id, ReviewInput newReview)
        {
            //Sér til þess að fylgst er með hvort bók sé breytt
            _db.Books.Attach(bookEntity);

            //Upplýsingar um bókina úr database vistaðar í bookEntity
            bookEntity = (from b in _db.Books
                                where b.ID == id.GetValueOrDefault()
                                select b).SingleOrDefault();

            //Nýja rating-inu bætt við bookEntity
            bookEntity.rating = bookEntity.rating + newReview.rating;
            //Total number of ratings í bookEntity hækkað um 1
            bookEntity.noOfRatings = bookEntity.noOfRatings + 1;
            
            

            //Review-ið vistað í reviewEntity
            reviewEntity.review = newReview.review;
            reviewEntity.rating = newReview.rating;
            reviewEntity.username = "Implement username here";
            reviewEntity.BookID = id.GetValueOrDefault(); //int? breytt í int
            _db.Reviews.Add(reviewEntity);

            //reviewEntity og bookEntity vistað í gagnagrunninum
            _db.SaveChanges();
        }

        public BookView GetBookDetail(int? id)
        {
            var bookDetail =(from b in _db.Books
                            where b.ID == id
                            select new BookView
                            {
                                ID = b.ID,
                                image = b.image,
                                author = b.author,
                                ISBN = b.ISBN,
                                title = b.title,
                                year = b.year,
                                numberOfPages = b.numberOfPages,
                                description = b.description,
                                country = b.country,
                                language = b.language,
                                publisher = b.publisher,
                                category = b.category,
                                noOfSoldUnits = b.noOfSoldUnits,
                                noOfCopiesAvailable = b.noOfCopiesAvailable,
                                //price og discount margfaldað saman til að fá raunverðið
                                price = b.price * b.discount,
                                discount = b.discount,
                                //Þarf að deila rating með noOfRatings til að fá average rating
                                rating = b.rating / b.noOfRatings,
                                noOfRatings = b.noOfRatings,
                                //Reviews = b.Reviews
                                Reviews = (from book in _db.Books
                                            join reviews in _db.Reviews on book.ID equals reviews.BookID
                                            where reviews.BookID == id.GetValueOrDefault()
                                            select reviews).ToList()
                            }).SingleOrDefault();

            return bookDetail;
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
                            discount = b.discount,
                            image = b.image
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
                                        rating = b.rating * b.noOfRatings,
                                        description = b.description,
                                        country = b.country,
                                        language = b.language,
                                        publisher = b.publisher,
                                        price = b.price * b.discount,
                                        category = b.category,
                                        noOfSoldUnits = b.noOfSoldUnits,
                                        noOfCopiesAvailable = b.noOfCopiesAvailable,
                                        discount = (1 - b.discount) * 100,
                                        image = b.image
                                    }).ToList();
            return discountedBooks;
        }

        public List<BookView> GetBooksByAuthor(string author)
        {
            var books = (from b in _db.Books
                        where author == b.author
                        select new BookView{
                                        ID = b.ID,
                                        author = b.author,
                                        title = b.title,
                                        rating = b.rating * b.noOfRatings,
                                        price = b.price * b.discount,
                                        category = b.category,
                                        noOfCopiesAvailable = b.noOfCopiesAvailable,
                                        discount = (1 - b.discount) * 100,
                                        image = b.image
                                    }).ToList();
            return books;
        }
    }
}