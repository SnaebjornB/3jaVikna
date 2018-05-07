using BookCave.Models.EntityModels;
using BookCave.Data;
using System.Linq;
using BookCave.Models.ViewModels;

namespace BookCave.Repositories
{
    public class OrderRepo
    {
        private DataContext _db;
        private OrderItemEntity orderItemEntity;
        public OrderRepo()
        {
            _db = new DataContext();
            orderItemEntity = new OrderItemEntity();
        }
        public void addToBasket(int bookID, string customerID)
        {
            var item = (from b in _db.Books
                        where b.ID == bookID
                        select b).Single();

            orderItemEntity.bookAuthor = item.author;
            orderItemEntity.bookID = item.ID;
            orderItemEntity.bookName = item.title;
            orderItemEntity.customerID = customerID;
            orderItemEntity.price = item.price;

            _db.Add(orderItemEntity);
            _db.SaveChanges();
        }

        public OrderBasketView getBasket(string customerID)
        {
            var item = (from i in _db.OrderItems
                        where i.customerID == customerID
                        select i).ToList();
            var orderbasketview = new OrderBasketView();

            foreach (var oneItem in item)
            {
                if(orderbasketview.books.Count() == 0)
                {
                    orderbasketview.books.Add(oneItem);
                }
                else
                {
                    foreach (var alreadyIn in orderbasketview.books)
                    {
                        if(oneItem.bookID == alreadyIn.bookID)
                        {
                            alreadyIn.quantity++;
                        }
                        else
                        {
                            orderbasketview.books.Add(oneItem);
                        }
                    }
                }
            }
            foreach (var basketItem in orderbasketview.books)
                {
                    orderbasketview.totalPrice += basketItem.price * basketItem.quantity;
                }

            return orderbasketview;
        }
        /*public BookEntity getItem(int bookID)
        {
            var item = (from b in _db.Books
                        where b.ID == bookID
                        select b).Single();

            return item;
        }*/
    }
}