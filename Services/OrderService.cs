using BookCave.Models.EntityModels;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class OrderService
    {
        public OrderRepo orderRepo;
        public OrderService()
        {
            orderRepo = new OrderRepo();
        }
        
        public void addToBasket(int bookID, int quantity, int customerID)
        {
            orderRepo.addToBasket(bookID, quantity, customerID);
        }

        public OrderBasketView getBasket(int customerID)
        {
            return orderRepo.getBasket(customerID);
        }
        /*public OrderItemEntity getItem(int bookID, int quantity)
        {
            var book = new BookEntity();
            book = orderRepo.getItem(bookID);
            var newItem = new OrderItemEntity();
            newItem.quantity = quantity;
            newItem.price = book.price;
            newItem.bookName = book.title;
            newItem.bookID = bookID;
            newItem.bookAuthor = book.author;

            return newItem;
        }*/
    }
}