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
                            select new AddressViewModel
                            {
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
                            select new EditAddressViewModel
                            {
                                ID = a.ID,
                                streetName = a.streetName,
                                houseNumber = a.houseNumber,
                                zip = a.zip,
                                city = a.city,
                                country = a.country
                            }).ToList();
            return addresses;
        }

        internal AddressViewModel GetViewAddressById(int id)
        {
            var result = (from a in _db.Addresses
                            where id == a.ID
                            select new AddressViewModel
                            {
                                city = a.city,
                                country = a.country,
                                zip = a.zip,
                                houseNumber = a.houseNumber,
                                streetName = a.streetName,
                                ID = a.ID
                            }).SingleOrDefault();
            return result;
        }

        internal CCardInfoViewModel GetCardById(int cardID)
        {
            var result = (from c in _db.Cards
                            where cardID == c.ID
                            select new CCardInfoViewModel
                            {
                                number = c.number,
                                month = c.month,
                                year = c.year
                            }).FirstOrDefault();
            return result;
        }

        internal void SaveCurrentOrderBooks(List<ReviewBookViewModel> books, string id)
        {
            var oldBooks = (from b in _db.CurrentOrderBooks
                            where id == b.userID
                            select b).ToList();
            if(oldBooks != null)
            {
                _db.RemoveRange(oldBooks);
                _db.AddRange(books);
                _db.SaveChanges();
            }
        }

        internal List<OrderHistoryBookViewModel> GetOrderHistoryBooksByTimeStamp(string timeStamp, string userId)
        {
            var books = (from b in _db.OrderHistoryBooks
                        where timeStamp == b.orderHistoryID && userId == b.userID
                        orderby b.ID descending
                        select new OrderHistoryBookViewModel{
                            bookID = b.bookID,
                            price = b.price,
                            quantity = b.quantity,
                            title = b.title,
                            author = b.author,
                            userID = b.userID,
                            orderHistoryID = b.orderHistoryID
                        }).ToList();
            return books;
        }

        internal List<OrderHistoryViewModel> GetOrderHistory(string userId)
        {
            var result = (from h in _db.OrderHistory
                            where userId == h.userID
                            select new OrderHistoryViewModel{
                                userID = h.userID,
                                timeStamp = h.timeStamp,
                                totalPrice = h.totalPrice,
                                address = h.address
                            }).ToList();
            return result;
        }

        internal void ConfirmCurrentOrder(string userID)
        {
            var currentOrder = (from o in _db.CurrentOrder
                                where userID == o.userID
                                select o).FirstOrDefault();

            var currentOrderBooks = (from b in _db.CurrentOrderBooks
                                    where userID == b.userID
                                    select b).ToList();

            if(currentOrder != null && currentOrderBooks != null)
            {
                var orderHistoryObject = new OrderHistoryEntity{
                    userID = currentOrder.userID,
                    totalPrice = currentOrder.totalPrice,
                    address = currentOrder.address,
                    timeStamp = DateTime.Now.ToString()
                };
                var orderHistoryBooks = new List<OrderHistoryBookViewModel>();
                foreach(var book in currentOrderBooks)
                {
                    orderHistoryBooks.Add(new OrderHistoryBookViewModel
                    {
                        userID = book.userID,
                        price = book.price,
                        quantity = book.quantity,
                        title = book.bookName,
                        author = book.bookAuthor,
                        bookID = book.bookID,
                        orderHistoryID = orderHistoryObject.timeStamp
                    });
                }
                _db.Remove(currentOrder);
                _db.RemoveRange(currentOrderBooks);
                _db.Add(orderHistoryObject);
                _db.AddRange(orderHistoryBooks);
                _db.SaveChanges();
            }
            else
            {
                throw new Exception("Something went wrong");
            }
        }

        internal void SaveCurrentOrder(ReviewViewModel reviewModel, string id)
        {
            var oldUnpaid = (from o in _db.CurrentOrder
                        where id == o.userID
                        select o).FirstOrDefault();
            if(oldUnpaid != null)
            {
                _db.Remove(oldUnpaid);
            }
            _db.Add(reviewModel);
            _db.SaveChanges();
        }

        internal List<CCardInfoViewModel> GetCards(string userID)
        {
            var result = (from c in _db.Cards
                            where userID == c.userID
                            select new CCardInfoViewModel
                            {
                                ID = c.ID,
                                number = c.number,
                                month = c.month,
                                year = c.year
                            }).ToList();
            return result;
        }

        public EditAddressViewModel GetAddressById(int id, string userID)
        {
            var result = (from a in _db.Addresses
                            where id == a.ID && userID == a.userID
                            select new EditAddressViewModel
                            {
                                city = a.city,
                                country = a.country,
                                zip = a.zip,
                                houseNumber = a.houseNumber,
                                streetName = a.streetName,
                                ID = a.ID
                            }).SingleOrDefault();
            return result;
        }

        internal void AddCard(CCardInfoViewModel newCCard, string id)
        {
            var card = new CCardEntity
            {
                userID = id,
                number = newCCard.number,
                month = newCCard.month,
                year = newCCard.year
            };
            _db.Add(card);
            _db.SaveChanges();
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
                            where id == a.ID && userId == a.userID
                            select a).FirstOrDefault();
            if(address != null)
            {
                _db.Remove(address);
                _db.SaveChanges();
            }
            throw new Exception("Address does not belong to this account or does not exist");
        }
    }
}