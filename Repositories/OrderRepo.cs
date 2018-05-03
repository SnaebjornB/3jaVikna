using BookCave.Models.EntityModels;
using BookCave.Models.ViewModels;
using BookCave.Data;
using System.Collections.Generic;
using System.Linq;

namespace BookCave.Repositories
{
    public class OrderRepo
    {
        private DataContext _db;
        public OrderRepo()
        {
            _db = new DataContext();
        }
        public BookEntity getItem(int bookID)
        {
            var item = (from b in _db.Books
                        where b.ID == bookID
                        select b).Single();

            return item;
        }
    }
}