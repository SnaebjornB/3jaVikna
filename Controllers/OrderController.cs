using BookCave.Models.EntityModels;
using BookCave.Models.ViewModels;
using BookCave.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookCave.Controllers
{
    public class OrderController : Controller
    {
        public OrderService orderService;
        public OrderBasketView orderBasketView = new OrderBasketView();
        public OrderController()
        {
            orderService = new OrderService();

        }
        [HttpGet]
        public IActionResult Basket()
        {
            Basket(1, 2);
            return View(orderBasketView);
        }

        [HttpPost]
        public IActionResult Basket(int bookID, int quantity)
        {
            var tempItem = new OrderItemEntity();
            tempItem = orderService.getItem(bookID, quantity);
            orderBasketView.books.Add(tempItem);
            orderBasketView.totalPrice += tempItem.price * quantity;

            return View(orderBasketView);
        }
    }
}