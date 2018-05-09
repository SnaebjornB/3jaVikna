using System.Collections.Generic;

namespace BookCave.Models.EntityModels
{
    public class OrderHistoryEntity
    {
        public string userID { get; set; }
        public int ID { get; set; }
        public double totalPrice { get; set; }
        public string address { get; set; }
        public string timeStamp { get; set; }
    }
}