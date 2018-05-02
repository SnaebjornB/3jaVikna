using System.Collections.Generic;
using BookCave.Models.ViewModels;

namespace BookCave.Repositories
{
    public class BookRepo
    {
        public List<BookView> GetTop10BooksFromDB()
        {
           // LINQ sem nær í BookEntity úr DB og breytir í BookView. Þangað til nota ég hér fake database.
           var books = new List<BookView> 
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
                    discount = 1
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
           return books;
        }
    }
}