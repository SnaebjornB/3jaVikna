namespace BookCave.Models.EntityModels
{
    public class OrderItemEntity
    {
        public int ID { get; set; }
        public int bookID { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public string bookName { get; set; }
        public string bookAuthor { get; set; }
    }
}