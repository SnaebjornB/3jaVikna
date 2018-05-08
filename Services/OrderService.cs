using System;
using System.Collections.Generic;
using BookCave.Models.EntityModels;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class OrderService
    {
        public OrderRepo orderRepo;
        private readonly AccountRepo _accountRepo;
        public OrderService()
        {
            orderRepo = new OrderRepo();
            _accountRepo = new AccountRepo();
        }
        
         public void addToBasket(int bookID, string customerID)
        {
            orderRepo.addToBasket(bookID, customerID);
        }

        public void deleteItemFromBasket(int bookID, string customerID)
        {
            orderRepo.deleteItemFromBasket(bookID, customerID);
        }

        public OrderBasketView getBasket(string customerID)
        {
            return orderRepo.getBasket(customerID);
        }

        public void clearBasket(string customerID)
        {
            orderRepo.clearBasket(customerID);
        }

         public void clearBookCopies(int bookID, string customerID)
        {
            orderRepo.clearBookCopies(bookID, customerID);
        }

        internal List<string> GetAddresses(string userID)
        {
            var addressStrings = new List<string>();
            var addresses = _accountRepo.GetAddresses(userID);
            foreach(var addr in addresses)
            {
                addressStrings.Add(addr.streetName.ToString() + " " + addr.houseNumber.ToString());
            }
            return addressStrings;
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