using System;
using System.Collections.Generic;
using BookCave.Models.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class OrderBasketView
    {
        public double totalPrice { get; set; }
        public List<OrderItemEntity> books { get; set; }
    }
}