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
        public void addToBasket(int bookID, int quantity, int customerID)
        {
            var item = (from b in _db.Books
                        where b.ID == bookID
                        select b).Single();

            orderItemEntity.bookAuthor = item.author;
            orderItemEntity.bookID = item.ID;
            orderItemEntity.bookName = item.title;
            orderItemEntity.customerID = customerID;
            orderItemEntity.price = item.price;
            orderItemEntity.quantity = quantity;

            _db.Add(orderItemEntity);
            _db.SaveChanges();
        }

        public OrderBasketView getBasket(int customerID)
        {
            var item = (from i in _db.OrderItems
                        where i.customerID == customerID
                        select i).ToList();
            var orderbasketview = new OrderBasketView();

            foreach (var oneItem in item)
            {
                orderbasketview.totalPrice += oneItem.price * oneItem.quantity;
                orderbasketview.books.Add(oneItem);
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