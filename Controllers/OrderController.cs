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

namespace BookCave.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderService orderService;
        public OrderBasketView orderBasketView = new OrderBasketView();
        public OrderController(UserManager<ApplicationUser> userManager)
        {
            orderService = new OrderService();
            _userManager = userManager;

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
    
