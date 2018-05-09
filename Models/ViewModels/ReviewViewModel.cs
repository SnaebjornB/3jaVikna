using System.Collections.Generic;
using BookCave.Models.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class ReviewViewModel
    {
        public string userID { get; set; }
        public int ID { get; set; }
        public double totalPrice { get; set; }
        public string address { get; set; }
        public string card { get; set; }
        public bool payPal { get; set; }
        public bool paid { get; set; }
    }
}