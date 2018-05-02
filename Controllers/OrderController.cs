using BookCave.Models.EntityModels;
using Microsoft.AspNetCore.Mvc;

namespace BookCave.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Basket()
        {
            return View();
        }

        /*public OrderItemEntity addToBasket()
        {
            
        }*/
    }
}