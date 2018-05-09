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

        public string GetCurrentUserId()
        {
            ClaimsPrincipal currentUser = this.User;
            string id = _userManager.GetUserId(currentUser);
            return id;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Something is wrong with the RegisterViewModel";
                return View();
            }
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                image = "https://cdn.pixabay.com/photo/2016/08/31/11/54/user-1633249_1280.png", //Er undir Creative Commons
                favBook = ""
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if(result.Succeeded) 
            {
                await _userManager.AddClaimAsync(user, new Claim("Name", $"{model.FirstName} {model.LastName}"));
                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home");
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
                ViewData["ErrorMessage"] = "Something is wrong with the LoginViewModel";
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
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
        [HttpPost]
        public async Task<bool> DoesEmailExist(string email)
        {
            var userExists = await _userManager.FindByNameAsync(email);
            if(userExists != null)
            {
                return false;
            }
            return true;
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            ClaimsPrincipal currentUser = this.User;
            ApplicationUser user = await _userManager.GetUserAsync(currentUser);
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
            if(ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if(user != null)
                {
                    user.image = model.image;
                    user.favBook = model.favBook;
                }
                IdentityResult result = await _userManager.UpdateAsync(user);
                Claim claim = ((ClaimsIdentity) User.Identity).Claims.FirstOrDefault(c => c.Type == "Name");
                await _userManager.RemoveClaimAsync(user, claim);
                await _userManager.AddClaimAsync(user, new Claim("Name", $"{model.firstName} {model.lastName}"));
                if(result.Succeeded){
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
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
            var address = _accountService.GetAddressById(id);
            
            return View(address);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAddress(EditAddressViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _accountService.UpdateAddress(model);
                    return RedirectToAction("EditAddresses");
                }
                catch(System.Exception)
                {
                    return View("Error", "Home");
                }
            }
            else
            {
                return View();
            }
        }
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
            }
            
            return View();
        }

        [Authorize]
        public IActionResult DeleteAddress(int id)
        {
            string userId = GetCurrentUserId();
            _accountService.DeleteAddress(id, userId);
            return RedirectToAction("EditAddresses");
        }
        [Authorize]
        public IActionResult OrderHistory()
        {
            var userId = GetCurrentUserId();
            var history = _accountService.GetOrderHistory(userId);
            return View(history);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}