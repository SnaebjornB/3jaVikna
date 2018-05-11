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
        public OrderService()
        {
            orderRepo = new OrderRepo();
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
            OrderBasketView _orderbasketview = new OrderBasketView();

            _orderbasketview = orderRepo.getBasket(customerID);

            foreach (var book in _orderbasketview.books)
            {
                book.price = Math.Round(book.price, 2);
            }

            Math.Round(_orderbasketview.totalPrice, 2);

            return _orderbasketview;
        }

        public void clearBasket(string customerID)
        {
            orderRepo.clearBasket(customerID);
        }

         public void clearBookCopies(int bookID, string customerID)
        {
            orderRepo.clearBookCopies(bookID, customerID);
        }

        public int countBasket(string customerID)
        {
            OrderBasketView _orderbasketview = orderRepo.getBasket(customerID);
            int counter = 0;

            foreach (var item in _orderbasketview.books)
            {
                counter += item.quantity;
            }
            return counter;
        }
    }
}