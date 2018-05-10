using System;
using System.Net.Http;
using System.Security.Claims;
using BookCave.Models.EntityModels;
using BookCave.Models.ViewModels;
using BookCave.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models.InputModels;
using System.Collections.Generic;

namespace BookCave.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly OrderService _orderService;
        private readonly AccountService _accountService;
        public OrderBasketView orderBasketView;
        public OrderController(UserManager<ApplicationUser> userManager)
        {
            _orderService = new OrderService();
            _userManager = userManager;
            _accountService = new AccountService();
            orderBasketView = new OrderBasketView();
        }

        public string GetCurrentUserId()
        {
            ClaimsPrincipal currentUser = this.User;
            string id = _userManager.GetUserId(currentUser);

            if(!string.IsNullOrEmpty(id))
            {
                return id;
            }
            else
            {
                throw new Exception("userID not found");
            }
        }
        
        [HttpGet]
        public IActionResult Basket()
        {
            string userID = GetCurrentUserId();

            if(!string.IsNullOrEmpty(userID))
            {
                orderBasketView = _orderService.getBasket(userID);
                return View(orderBasketView);
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public bool addToBasket(int bookID)
        {
            string userID = GetCurrentUserId();

            if(!string.IsNullOrEmpty(userID))
            {
                _orderService.addToBasket(bookID, userID);
            }
            else
            {
                RedirectToAction("~/Views/Login");
            }

            return true;
        }

        [HttpPost]
        public bool deleteItemFromBasket(int bookID)
        {
            string userID = GetCurrentUserId();

            if(!string.IsNullOrEmpty(userID))
            {
                _orderService.deleteItemFromBasket(bookID, userID);
            }
            else
            {
                return false;
            }
            
            return true;
        }

        [HttpPost]
        public bool clearBasket()
        {
            string userID = GetCurrentUserId();

            if(!string.IsNullOrEmpty(userID))
            {
                _orderService.clearBasket(userID);
            }
            else
            {
                return false;
            }

            return true;
        }

        [HttpPost]
        public bool clearBookCopies(int bookID)
        {
            string userID = GetCurrentUserId();

            if(!string.IsNullOrEmpty(userID))
            {
                _orderService.clearBookCopies(bookID, userID);
            }
            else
            {
                return false;
            }
            
            return true;
        }

        [HttpPost]
        public IActionResult Billing()
        {
            string userID = GetCurrentUserId();
            var basket = _orderService.getBasket(userID);

            if(!string.IsNullOrEmpty(userID))
            {
                var addressStrings = _accountService.GetAddresses(userID);
                var Ccards = _accountService.GetCards(userID);
                var bask = new BillingModelView{
                                totalPrice = basket.totalPrice,
                                books = basket.books,
                                addresses = addressStrings,
                                cards = Ccards
                            };

                if(bask.books != null)
                {
                    return View(bask);
                }
                else
                {
                    return RedirectToAction("Basket");
                } 
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public IActionResult Review(BillingModelView model){
            var ReviewModel = new ReviewViewModel();
            string userID = GetCurrentUserId();
            var basket = _orderService.getBasket(userID);
            var currentOrderBooks = new List<ReviewBookViewModel>();

            if(!string.IsNullOrEmpty(userID))
            {
                foreach(var item in basket.books)
                {
                    currentOrderBooks.Add(new ReviewBookViewModel
                    {
                        bookID = item.bookID,
                        bookName = item.bookName,
                        bookAuthor = item.bookAuthor,
                        userID = item.customerID,
                        quantity = item.quantity,
                        price = item.price
                    });
                }

                ReviewModel.totalPrice = basket.totalPrice;
            }

            if(model.address == "newAddress")
            {
                var newAddress = new EditAddressViewModel
                {
                    streetName = model.newStreetName,
                    houseNumber = model.newHouseNumber,
                    zip = model.newZip,
                    city = model.newCity,
                    country = model.newCountry
                };
                _accountService.AddAddress(newAddress, userID);
                ReviewModel.address = model.newStreetName + " " + model.newHouseNumber + ", "
                                    + model.newZip + " " + model.newCity + ", " + model.newCountry;
            }
            else
            {
                var tempAddress = _accountService.GetViewAddressById(model.addressID);
                ReviewModel.address = tempAddress.streetName + " " + tempAddress.houseNumber + ", "
                                    + tempAddress.zip + " " + tempAddress.city + ", " + tempAddress.country;
            }

            if(model.cCard == "newCCard")
            {
                if(!string.IsNullOrEmpty(model.newNumber))
                {
                    var newCard = new CCardInfoViewModel
                    {
                        number = model.newNumber,
                        month = model.newMonth,
                        year = model.newYear 
                    };

                    if(model.saveCCard == true)
                    {
                        _accountService.AddCard(newCard, userID);
                    }

                    var card_number = newCard.number;
                    var cardNumber = String.Format(card_number.Substring(card_number.Length-4));
                    ReviewModel.card = cardNumber;
                }
                else
                {
                    ViewData["CardErrorMessage"] = "Please make sure all the fields are filled out correctly and try again";
                    return RedirectToAction("Billing");
                }
            }
            else if(model.cCard == "existingCCard")
            {
                var tempCard = _accountService.GetCardById(model.cardID);

                if(tempCard != null)
                {
                    var card_number = tempCard.number;
                    var cardNumber = String.Format(card_number.Substring(card_number.Length-4));
                    ReviewModel.card = cardNumber;
                }
                else
                {
                    ViewData["ErrorMessage"] = "Something went wrong. Please make sure all the fields are filled out correctly and try again";
                    return RedirectToAction("Billing");
                }
            }
            else
            {
                ReviewModel.payPal = true;
            }

            ReviewModel.userID = userID;
            _accountService.SaveCurrentOrder(ReviewModel, currentOrderBooks, userID);

            var result = new ReviewStepViewModel{
                totalPrice = ReviewModel.totalPrice,
                address = ReviewModel.address,
                card = ReviewModel.card,
                payPal = ReviewModel.payPal,
                books = currentOrderBooks
            };
            
            return View(result);
        }

        public IActionResult Confirm()
        {
            string userID = GetCurrentUserId();
            if(!string.IsNullOrEmpty(userID))
            {
                _accountService.ConfirmCurrentOrder(userID);
                _orderService.clearBasket(userID);
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
    
