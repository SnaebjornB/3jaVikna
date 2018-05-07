using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models;
using BookCave.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System;
using BookCave.Models.EntityModels;

namespace BookCave.Repositories
{
    public class AccountRepo
    {
        private DataContext _db = new DataContext();
        public List<AddressViewModel> GetAddresses(string id)
        {
            var addresses = (from a in _db.Addresses
                            where id == a.userID 
                            select new AddressViewModel{
                                ID = a.ID,
                                streetName = a.streetName,
                                houseNumber = a.houseNumber,
                                zip = a.zip,
                                city = a.city,
                                country = a.country
                            }).ToList();
            return addresses;
        }    
        public List<EditAddressViewModel> GetAddressesEdit(string id)
        {
            var addresses = (from a in _db.Addresses
                            where id == a.userID 
                            select new EditAddressViewModel{
                                ID = a.ID,
                                streetName = a.streetName,
                                houseNumber = a.houseNumber,
                                zip = a.zip,
                                city = a.city,
                                country = a.country
                            }).ToList();
            return addresses;
        }    
        public EditAddressViewModel GetAddressById(int id)
        {
            var result = (from a in _db.Addresses
                            where id == a.ID
                            select new EditAddressViewModel{
                                city = a.city,
                                country = a.country,
                                zip = a.zip,
                                houseNumber = a.houseNumber,
                                streetName = a.streetName,
                                ID = a.ID
                            }).SingleOrDefault();
            return result;
        }
        public void UpdateAddress(EditAddressViewModel model)
        {
            var existing = _db.Addresses.SingleOrDefault(a => a.ID == model.ID);
            if(existing != null)
            {
                existing.country = model.country;
                existing.city = model.city;
                existing.zip = model.zip;
                existing.houseNumber = model.houseNumber;
                existing.streetName = model.streetName;
                _db.SaveChanges();
            }
            else
            {
                throw new Exception("EditAddressViewModel is empty");
            }
        }
        public void AddAddress(EditAddressViewModel model, string id)
        {
            var newAddress = new AddressEntity{
                userID = id,
                country = model.country,
                city = model.city,
                zip = model.zip,
                houseNumber = model.houseNumber,
                streetName = model.streetName
            };
            _db.Add(newAddress);
            _db.SaveChanges();
        }
        public void DeleteAddress(int id, string userId)
        {
            var address = (from a in _db.Addresses
                            where id == a.ID
                            select a).FirstOrDefault();
            if(address.userID != userId)
            {
                throw new Exception("This address does not belong to you account");
            }
            else
            {
                _db.Remove(address);
                _db.SaveChanges();
            }
        }
    }
}