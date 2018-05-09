using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookCave.Models.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class BillingModelView
    {
        public double totalPrice { get; set; }
        public List<OrderItemEntity> books { get; set; }
        public List<AddressViewModel> addresses { get; set; }
        public List<CCardInfoViewModel> cards { get; set; }

        public int addressID { get; set; }
        public string newCountry { get; set; }
        public string newCity { get; set; }
        public int newZip { get; set; }
        public string newStreetName { get; set; }
        public string newHouseNumber { get; set; }

        [RegularExpression("^[0-9]{0,16}$")]
        public string newNumber { get; set; }
        public int newMonth { get; set; }
        public int newYear { get; set; }

        public int cardID { get; set; }
        public string address { get; set; }
        public string cCard { get; set; }
        public bool saveCCard { get; set; }
    }
}