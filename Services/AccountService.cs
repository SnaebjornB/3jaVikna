using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class AccountService
    {
        private AccountRepo _accountRepo;

        public AccountService()
        {
            _accountRepo = new AccountRepo();
        }

        public List<string> GetAddressStrings(string id)
        {
            var addresses = _accountRepo.GetAddresses(id);
            if(addresses != null){
                var addressStrings = new List<string>();
                foreach (var address in addresses)
                {
                    addressStrings.Add(address.streetName + " " + address.houseNumber  + ", " + 
                    address.zip + " " + address.city + ", " + address.country);
                }
                return addressStrings;
            }
            else
            {
                return null;
            }
        }

        public List<AddressViewModel> GetAddresses(string id)
        {
            return _accountRepo.GetAddresses(id);
        }
        public List<EditAddressViewModel> GetAddressesEdit(string id)
        {
            return _accountRepo.GetAddressesEdit(id);
        }
        public EditAddressViewModel GetAddressById(int id)
        {
            return _accountRepo.GetAddressById(id);
        }
        public void UpdateAddress(EditAddressViewModel model)
        {
            _accountRepo.UpdateAddress(model);
        }
        public void AddAddress(EditAddressViewModel model, string id)
        {
            _accountRepo.AddAddress(model, id);
        }

        public void DeleteAddress(int id, string userID)
        {
            _accountRepo.DeleteAddress(id, userID);
        }
    }
}
