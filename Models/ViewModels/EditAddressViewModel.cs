using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.ViewModels
{
    public class EditAddressViewModel    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string country { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string city { get; set; }
        [Required(ErrorMessage = "ZIP code is required")]
        public int zip { get; set; }
        [Required(ErrorMessage = "Street name is required")]
        public string streetName { get; set; }
        public string houseNumber { get; set; } //string því það getur innihaldið staf.
    }
}