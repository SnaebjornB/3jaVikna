using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;
using BookCave.Models.InputModels;
using Microsoft.AspNetCore.Identity;
using BookCave.Models.EntityModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BookCave.Controllers
{
    public class BookController : Controller
    {
        private BookServices _bookService;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookController(UserManager<ApplicationUser> userManager)
        {
            _bookService = new BookServices();
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string searchTitle, string searchAuthor, string searchISBN, string searchCategory, string orderBy)
        {
            var searchResult = _bookService.GetSearchResult(searchTitle, searchAuthor, searchISBN, searchCategory, orderBy);
            return View(searchResult);
        }

        [HttpPost]
        public IActionResult SearchBar(string searchWord)
        {
            var searchResult = _bookService.GetSearchResultForBar(searchWord);
            return View("./Search", searchResult);
        }

        public IActionResult Top10()
        {
            var top10BookList = _bookService.GetTop10HighestRated();
            return View(top10BookList);
        }

        [HttpGet]
        public IActionResult Detail(int? id)
        {
            if(id == null){
                return View("NotFound");
            }

            var bookDetail = _bookService.GetBookDetail(id);

            
            if(bookDetail == null){
                return View("NotFound");
            }

            return View(bookDetail);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Review(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            return View();
        }
        
        
        [HttpPost]
        [Authorize]
        public IActionResult Review(int? id, ReviewInput newReview)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            //Athuga hvort að bók með ID == id sé til
            if(!_bookService.IsBookInDatabase(id)){
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                //Fáum hvaða user sé að skrifa review
                ClaimsPrincipal currentUser = this.User;
                string userID = _userManager.GetUserId(currentUser);
                var userName = ((ClaimsIdentity) User.Identity).Claims.FirstOrDefault(c => c.Type == "Name")?.Value;

                //Ef user-inn er til
                _bookService.AddReview(id, newReview, userID, userName);
                return RedirectToAction("Search");
                //ef það er gestur að skrifa review

            }
            
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult DiscountedBooks()
        {
            var books = _bookService.GetAllDiscountedBooks();
            return View(books);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult AuthorDetail(string author)
        {
            if(string.IsNullOrEmpty(author)){
                return View("NotFound");
            }

            var bookDetail = _bookService.GetBooksByAuthor(author);

            
            if(bookDetail == null){
                return View("NotFound");
            }

            return View(bookDetail);
        }
        [HttpGet]
        public IActionResult ShopByCategory(){
            return View();
        }
    }
}

