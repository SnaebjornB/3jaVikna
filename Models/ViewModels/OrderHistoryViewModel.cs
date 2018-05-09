using System.Collections.Generic;

namespace BookCave.Models.ViewModels
{
    public class OrderHistoryViewModel
    {
        public string userID { get; set; }
        public int ID { get; set; }
        public List<OrderHistoryBookViewModel> books { get; set; }
        public double totalPrice { get; set; }
        public string address { get; set; }
        public string timeStamp { get; set; }
    }
}