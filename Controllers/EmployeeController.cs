using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookCave.Models.EntityModels;
using BookCave.Models.InputModels;
using BookCave.Models.ViewModels;
using BookCave.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookCave.Controllers
{
    [Authorize(Roles="Employee")]
    public class EmployeeController : Controller
    {
        private BookServices _bookService;
        public EmployeeController(UserManager<ApplicationUser> userManager)
        {
            _bookService = new BookServices();
        }

        public IActionResult AddBook()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddBook(BookInputModel book)
        {
            if(ModelState.IsValid)
            {
                _bookService.AddNewBook(book);
                //confirmation message
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //error message
                return View(book);
            }
        }
        public IActionResult EditBook(int id)
        {
            var book = _bookService.GetBookToEdit(id);
            if(book != null)
            {
                return View(book); 
            }
            else
            {
                return View();
            }
        }
        
        [HttpPost]
        public IActionResult EditBook(BookInputModel book)
        {
            if(ModelState.IsValid)
            {
                _bookService.UpdateBook(book);
                //confirmation message
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //error message
                return View(book);
            }
        }

        public IActionResult EditDiscount(string searchTitle, string searchAuthor, string searchISBN, string searchCategory, string orderBy, int searchYearFrom, int searchYearTo)
        {
            var searchResult = _bookService.GetSearchResult(searchTitle, searchAuthor, searchISBN, searchCategory, orderBy, searchYearFrom, searchYearTo);
            return View(searchResult);
        }

        public IActionResult ApplyNewDiscount(List<BookView> books, double discount)
        {
            if(ModelState.IsValid)
            {
                _bookService.EditDiscount(books, discount);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}