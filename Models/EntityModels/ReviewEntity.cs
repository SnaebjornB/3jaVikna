namespace BookCave.Models.EntityModels
{
    public class ReviewEntity{
        public int ID { get; set; }
        public int BookID { get; set; }
        public string username { get; set; }
        public string review { get; set; }
        public double rating { get; set; }
    }
}