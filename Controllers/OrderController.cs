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
        public OrderService orderService;
        private readonly AccountService _accountService;
        public OrderBasketView orderBasketView = new OrderBasketView();
        public OrderController(UserManager<ApplicationUser> userManager)
        {
            orderService = new OrderService();
            _userManager = userManager;
            _accountService = new AccountService();

        }
        
        [HttpGet]
        public IActionResult Basket()
        {
            ClaimsPrincipal currentUser = this.User;
            string userID = _userManager.GetUserId(currentUser);
            orderBasketView = orderService.getBasket(userID);

            return View(orderBasketView);
        }

        [HttpPost]
        public bool addToBasket(int bookID)
        {
            ClaimsPrincipal currentUser = this.User;
            string userID = _userManager.GetUserId(currentUser);
            if(userID != null)
            {
                orderService.addToBasket(bookID, userID);
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
            ClaimsPrincipal currentUser = this.User;
            string userID = _userManager.GetUserId(currentUser);
            if(userID != null)
            {
                orderService.deleteItemFromBasket(bookID, userID);
            }
            
            return true;
        }

        [HttpPost]
        public bool clearBasket()
        {
            ClaimsPrincipal currentUser = this.User;
            string userID = _userManager.GetUserId(currentUser);
            if(userID != null)
            {
                orderService.clearBasket(userID);
            }

            return true;
        }
        [HttpPost]
        public bool clearBookCopies(int bookID)
        {
            ClaimsPrincipal currentUser = this.User;
            string userID = _userManager.GetUserId(currentUser);
            if(userID != null)
            {
                orderService.clearBookCopies(bookID, userID);
            }
            
            return true;
        }
        [HttpPost]
        [Authorize]
        public IActionResult Billing()
        {
            ClaimsPrincipal currentUser = this.User;
            string userID = _userManager.GetUserId(currentUser);
            var basket = orderService.getBasket(userID);
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
        [Authorize]
        [HttpPost]
        public IActionResult Review(BillingModelView model){
            var ReviewModel = new ReviewViewModel();
            ClaimsPrincipal currentUser = this.User;
            string userID = _userManager.GetUserId(currentUser);
            var basket = orderService.getBasket(userID);
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
            else if(model.cCard == "existingCCard")
            {
                var tempCard = _accountService.GetCardById(model.cardID);
                var card_number = tempCard.number;
                var cardNumber = String.Format(card_number.Substring(card_number.Length-4));
                ReviewModel.card = cardNumber;
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
        [Authorize]

        public IActionResult Confirm()
        {
            ClaimsPrincipal currentUser = this.User;
            string userID = _userManager.GetUserId(currentUser);
            _accountService.ConfirmCurrentOrder(userID);
            orderService.clearBasket(userID);
            return View();
        }
    }
}
                    /*public HttpResponseMessage addToBasket(int bookID, int quantity)
                    {
                        var tempItem = new OrderItemEntity();
                        tempItem = orderService.getItem(bookID, quantity);
                        orderBasketView.books.Add(tempItem);
                        orderBasketView.totalPrice += tempItem.price * quantity;

                        return View(orderBasketView);
                    */
    
