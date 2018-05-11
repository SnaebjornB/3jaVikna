﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;
using Microsoft.AspNetCore.Diagnostics;
using BookCave.Models.InputModels;

namespace BookCave.Controllers
{
    public class HomeController : Controller
    {
        private BookServices _bookService;

        public HomeController()
        {
            _bookService = new BookServices();
        }
        
        public IActionResult Index()
        {
            var allBooks = _bookService.GetAllBooks();
            return View(allBooks);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult TermsAndConditions()
        {
            return View();
        }

        public IActionResult WeDeliver()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Apply()
        {            
            return View();
        }

        [HttpPost]
        public IActionResult Apply(JobApplicationInput newApplication)
        {
            if (ModelState.IsValid)
            {
                //vistum application í database
                _bookService.AddJobApplication(newApplication);
                
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Error()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if(exceptionFeature != null)
            {
                string path = exceptionFeature.Path;
                Exception ex = exceptionFeature.Error;
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
