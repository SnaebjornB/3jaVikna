namespace BookCave.Models.ViewModels
{
    public class BookView{
        public int ID { get; set; }
        public string author { get; set; }
        public string ISBN { get; set; }
        public string title { get; set; }
        public int year { get; set; }
        public int numberOfPages { get; set; }
        public double rating { get; set; }
        public string description { get; set; }
        public string country { get; set; }
        public string language { get; set; }
        public string publisher { get; set; }
        public double price { get; set; }
        public string category { get; set; }
        public int noOfSoldUnits { get; set; }
        public int noOfCopiesAvailable { get; set; }
        public double discount { get; set; }
    }
}
