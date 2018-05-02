using System.Collections.Generic;
using System.Linq;
using BookCave.Models.ViewModels;

namespace BookCave.Repositories
{
    public class BookRepo
    {

        public List<BookView> books = new List<BookView> 
           {
               new BookView
               { 
                    ID = 1,
                    author = "Jón Jónsson", 
                    ISBN = 123456789, title = "The Book", 
                    year = 2000, 
                    numberOfPages = 1000, 
                    rating = 5, 
                    description = "this is a book", 
                    country = "Iceland",
                    price = 20, 
                    category = "Action", 
                    noOfSoldUnits = 1, 
                    noOfCopiesAvailable = 1, 
                    discount = 1
               },
               new BookView
               { 
                    ID = 2,
                    author = "Jón Jónsson", 
                    ISBN = 123456789, title = "The Book2", 
                    year = 2000, 
                    numberOfPages = 1000, 
                    rating = 5, 
                    description = "this is a book", 
                    country = "Iceland",
                    price = 20, 
                    category = "Action", 
                    noOfSoldUnits = 1, 
                    noOfCopiesAvailable = 1, 
                    discount = 1
               },
               new BookView
               { 
                    ID = 3,
                    author = "Jón Jónsson", 
                    ISBN = 123456789, title = "The Book3", 
                    year = 2000, 
                    numberOfPages = 1000, 
                    rating = 5, 
                    description = "this is a book", 
                    country = "Iceland",
                    price = 20, 
                    category = "Action", 
                    noOfSoldUnits = 1, 
                    noOfCopiesAvailable = 1, 
                    discount = 0.85
               },
               new BookView
               { 
                    ID = 4,
                    author = "Jón Jónsson", 
                    ISBN = 123456789, title = "The Book4", 
                    year = 2000, 
                    numberOfPages = 1000, 
                    rating = 5, 
                    description = "this is a book", 
                    country = "Iceland",
                    price = 20, 
                    category = "Action", 
                    noOfSoldUnits = 1, 
                    noOfCopiesAvailable = 1, 
                    discount = 1
               },
           };
        public List<BookView> GetTop10BooksFromDB()
        {
           // LINQ sem nær í BookEntity úr DB og breytir í BookView. Þangað til nota ég hér fake database.
           
           return books;
        }

        public List<BookView> GetAllDiscountedBooks(){
            var discountedBooks = (from b in books 
                                    where b.discount != 1   //Finnur bara allar bækur með afslætti, þarf líklega að fínpússa.
                                    select b).ToList();
            return discountedBooks;
        }
    }
}