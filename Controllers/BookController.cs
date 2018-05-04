using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;

namespace BookCave.Controllers
{
    public class BookController : Controller
    {
        private BookServices _bookService;

        public BookController()
        {
            _bookService = new BookServices();
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

        public IActionResult Top10()
        {
            var top10BookList = _bookService.GetTop10HighestRated();
            return View(top10BookList);
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

