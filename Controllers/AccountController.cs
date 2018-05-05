using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookCave.Models;
using BookCave.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookCave.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
                image = "",
                address1 = "",
                address2 = "",
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
                address1 = user.address1,
                address2 = user.address2,
                favBook = user.favBook
            };
            return View(model);
        }

      /*  [Authorize]
        public IActionResult GetEditProfile()
        {
            ClaimsPrincipal currentUser = this.User;
            string id = _userManager.GetUserId(currentUser);
            if(!string.IsNullOrEmpty(id)){
                  
            }
            else
            {
                return RedirectToAction("Profile");
            }
        }*/

        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            ClaimsPrincipal currentUser = this.User;
            string id = _userManager.GetUserId(currentUser);
            EditUserViewModel model = new EditUserViewModel();

            if(!string.IsNullOrEmpty(id))
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if(user != null)
                {
                    model.image = user.image;
                    model.favBook = user.favBook;
                    model.address1 = user.address1;
                    model.address2 = user.address2;
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
        public async Task<IActionResult> EditProfile(EditUserViewModel model)
        {
            ClaimsPrincipal currentUser = this.User;
            string id = _userManager.GetUserId(currentUser);
            if(ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if(user != null)
                {
                    user.image = model.image;
                    user.favBook = model.favBook;
                    user.address1 = model.address1;
                    user.address2 = model.address2;
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
    }
}