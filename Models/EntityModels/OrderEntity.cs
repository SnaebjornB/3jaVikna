using System;
using System.Collections.Generic;

namespace BookCave.Models.EntityModels
{
    public class OrderEntity
    {
        public OrderItemEntity newAdditionToBasket { get; set; }
        public double totalPrice { get; set; }
        public DateTime dateOfPurchase { get; set; }
        public List<OrderItemEntity> books { get; set; }
    }
}