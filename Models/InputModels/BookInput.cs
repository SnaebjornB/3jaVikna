namespace BookCave.Models.InputModels
{
    public class BookInput{
        public string author { get; set; }
        public int ISBN { get; set; }
        public string title { get; set; }
        public int year { get; set; }
        public int numberOfPages { get; set; }
        public string description { get; set; }
        public string country { get; set; }
        public string language { get; set; }
        public string publisher { get; set; }
        public double price { get; set; }
        public string category { get; set; }
        public int noOfCopiesAvailable { get; set; }
    }
}
