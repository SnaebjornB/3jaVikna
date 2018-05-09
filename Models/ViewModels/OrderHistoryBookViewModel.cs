namespace BookCave.Models.ViewModels
{
    public class OrderHistoryBookViewModel
    {
        public int ID { get; set; }
        public int bookID { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string userID { get; set; }
        public string orderHistoryID { get; set; }
    }
}