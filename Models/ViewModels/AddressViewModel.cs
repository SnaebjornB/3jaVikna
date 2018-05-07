namespace BookCave.Models.ViewModels
{
    public class AddressViewModel
    {
        public int ID { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public int zip { get; set; }
        public string streetName { get; set; }
        public string houseNumber { get; set; } //string því það getur innihaldið staf.
    }
}