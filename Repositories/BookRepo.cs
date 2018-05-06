using System;
using System.Collections.Generic;
using System.Linq;
using BookCave.Data;
using BookCave.Models.EntityModels;
using BookCave.Models.InputModels;
using BookCave.Models.ViewModels;

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
                                    discount = b.discount
                                }).ToList();

            return searchResult;
        }

        public void AddReview(int? id, ReviewInput newReview)
        {
            //Bókin fundin í Database?
            _db.Books.Attach(bookEntity);

            //Gildunum í Review-inu bætt við viðeigandi bók
            bookEntity = (from b in _db.Books
                                where b.ID == id.GetValueOrDefault()
                                select b).SingleOrDefault();
            //Búið að finna bókina sem verið er að review-a. Breytum henni.
            //Nýja rating-inu bætt við reviewedBook.rating
            bookEntity.rating = bookEntity.rating + newReview.rating;
            //Total number of ratings hækkað um 1
            bookEntity.noOfRatings = bookEntity.noOfRatings + 1;
            
            

            //Review-ið vistað í gagnagrunninn
            reviewEntity.review = newReview.review;
            reviewEntity.rating = newReview.rating;
            reviewEntity.username = "Implement username here";
            reviewEntity.BookID = id.GetValueOrDefault(); //int? breytt í int
            _db.Reviews.Add(reviewEntity);

            //Allar breytingar save-aðar í gagnagrunninum
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