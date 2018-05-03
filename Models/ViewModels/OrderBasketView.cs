using System;
using System.Collections.Generic;
using BookCave.Models.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class OrderBasketView
    {
        public OrderBasketView()
        {
            totalPrice = 0;
            books = new List<OrderItemEntity>();
        }
        public double totalPrice { get; set; }
        public List<OrderItemEntity> books { get; set; }
    }
}