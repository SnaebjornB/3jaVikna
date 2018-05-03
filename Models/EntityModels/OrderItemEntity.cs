namespace BookCave.Models.EntityModels
{
    public class OrderItemEntity
    {
        public OrderItemEntity()
        {
            ID = IDcounter;
            IDcounter++;
        }
        private static int IDcounter = 0;
        public int ID { get; set; }
        public int bookID { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public string bookName { get; set; }
        public string bookAuthor { get; set; }
    }
}