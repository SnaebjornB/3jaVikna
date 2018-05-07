namespace BookCave.Models.EntityModels
{
    public class OrderItemEntity
    {
        public OrderItemEntity()
        {
            quantity = 1;
        }
        public int ID { get; set; }
        public int bookID { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public string bookName { get; set; }
        public string bookAuthor { get; set; }
        public string customerID { get; set; }
    }
}