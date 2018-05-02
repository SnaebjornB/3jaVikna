using System;

namespace BookCave.Models.EntityModels
{
    public class OrderEntity
    {
        public double totalPrice { get; set; }
        public DateTime dateOfPurchase { get; set; }
        //public CCard creditCard { get; set; }
    }
}