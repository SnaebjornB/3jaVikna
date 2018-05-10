using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookCave.Models.EntityModels;
using BookCave.Models.ViewModels;
using BookCave.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookCave.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AccountService _accountService;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _accountService = new AccountService();
        }

        [Authorize]
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

        public IActionResult Register()
        {
            ClaimsPrincipal currentUser = this.User;

            if(_signInManager.IsSignedIn(currentUser))
            {
                return RedirectToAction("Profile");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Something went wrong. Please make sure all the fields are filled out correctly and try again";
                return View();
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                image = "https://cdn.pixabay.com/photo/2016/08/31/11/54/user-1633249_1280.png", //Er undir Creative Commons
                favBook = ""
            };
            var existingUser = await _userManager.FindByEmailAsync(model.Email);

            if(existingUser != null)
            {
                ViewData["ErrorMessage"] = "There is already an account with that email";
            }
            else
            {
                if(model.Password == model.confirmPassword)
                {
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if(result.Succeeded) 
                    {
                        await _userManager.AddClaimAsync(user, new Claim("Name", $"{model.FirstName} {model.LastName}"));
                        await _signInManager.SignInAsync(user, false);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "Your password does not meet the requirements";
                    }
                }    
                else
                {
                    ViewData["ErrorMessage"] = "Your passwords don't match";
                }
            }

            return View();
        }

        public IActionResult Login()
        {
            ClaimsPrincipal currentUser = this.User;

            if(_signInManager.IsSignedIn(currentUser))
            {
                return RedirectToAction("Profile");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Something is wrong went wrong, please try again";
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["ErrorMessage"] = "Email or password is not correct.";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult forgottenPassword()
        {
            return View();
        }

        [HttpGet]
        public async Task<ApplicationUser> DoesEmailExist(string email)
        {
            var userExists = await _userManager.FindByNameAsync(email);
            if(userExists == null)
            {
                return null;
            }

            return userExists;
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            ClaimsPrincipal currentUser = this.User;
            ApplicationUser user = await _userManager.GetUserAsync(currentUser);

            if(user == null)
            {
                return RedirectToAction("Login");
            }

            var model = new UserViewModel{
                name = ((ClaimsIdentity) User.Identity).Claims.FirstOrDefault(c => c.Type == "Name").ToString(),
                image = user.image,
                addresses = _accountService.GetAddressStrings(user.Id),
                favBook = user.favBook
            };
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            string id = GetCurrentUserId();
            EditUserViewModel model = new EditUserViewModel();

            if(!string.IsNullOrEmpty(id))
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);

                if(user != null)
                {
                    model.image = user.image;
                    model.favBook = user.favBook;
                    var claim = ((ClaimsIdentity) User.Identity).Claims.FirstOrDefault(c => c.Type == "Name").ToString();
                    var name = claim.Split(' ');
                    model.firstName = name[1];
                    model.lastName = name[2];
                }
                else
                {
                    return RedirectToAction("Profile");
                }
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditUserViewModel model)
        {
            string id = GetCurrentUserId();

            if(!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Something went wrong. Make sure all fields are filled.";
            }
            else
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if(user != null)
                {
                    user.image = model.image;
                    user.favBook = model.favBook;
                }

                IdentityResult result = await _userManager.UpdateAsync(user);

                if(result.Succeeded){
                    Claim claim = ((ClaimsIdentity) User.Identity).Claims.FirstOrDefault(c => c.Type == "Name");
                    await _userManager.RemoveClaimAsync(user, claim);
                    await _userManager.AddClaimAsync(user, new Claim("Name", $"{model.firstName} {model.lastName}"));

                    return RedirectToAction("Profile");
                }
            }

            return View(model); 
        }

        [Authorize]
        public IActionResult EditAddresses()
        {
            string id = GetCurrentUserId();

            if(!string.IsNullOrEmpty(id))
            {
                var addresses = _accountService.GetAddressesEdit(id);
                return View(addresses);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [Authorize]
        public IActionResult EditAddress(int id)
        {
            string userID = GetCurrentUserId();
            var address = _accountService.GetAddressById(id, userID);

            if(address == null)
            {
                return RedirectToAction("EditAddresses");
            }

            return View(address);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAddress(EditAddressViewModel model)
        {
            if(ModelState.IsValid)
            {
                _accountService.UpdateAddress(model);
                return RedirectToAction("EditAddresses");               
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public IActionResult AddAddress()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult AddAddress(EditAddressViewModel model)
        {
            string id = GetCurrentUserId();

            if(!string.IsNullOrEmpty(id))
            {
                if(ModelState.IsValid)
                {
                    _accountService.AddAddress(model, id);
                    return RedirectToAction("EditAddresses");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Something went wrong. Make sure all fields are filled.";
                    return View();
                }
            }
            
            return RedirectToAction("Login");
        }

        [Authorize]
        public IActionResult DeleteAddress(int id)
        {
            string userId = GetCurrentUserId();

            try
            {
                _accountService.DeleteAddress(id, userId);
            }
            catch(Exception e)
            {
                ViewData["ErrorMessage"] = e.ToString();
            }

            return RedirectToAction("EditAddresses");
        }

        [Authorize]
        public IActionResult OrderHistory()
        {
            var userId = GetCurrentUserId();

            if(string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login");
            }
            
            var history = _accountService.GetOrderHistory(userId);
            return View(history);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}