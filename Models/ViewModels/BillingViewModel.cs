using System;
using System.Collections.Generic;
using BookCave.Models.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class BillingModelView
    {
        public double totalPrice { get; set; }
        public List<OrderItemEntity> books { get; set; }
        public List<string> addresses { get; set; }
        public List<CCardInfoViewModel> cards { get; set; }
    }
}