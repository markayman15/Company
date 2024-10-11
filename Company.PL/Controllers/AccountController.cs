using Company.DAL.Models;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInMnager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInMnager)
        {
            this.userManager = userManager;
            this.signInMnager = signInMnager;
            //this.signInMnager = signInMnager;
        }
        public IActionResult SignUp()
        {
            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel SignUpVM)
        {
            if (ModelState.IsValid)
            {
                var check = await userManager.FindByEmailAsync(SignUpVM.Email);
                if (check == null)
                {
                    var AppUser = new ApplicationUser()
                    {
                        UserName = SignUpVM.UserName,
                        Email = SignUpVM.Email,
                        FName = SignUpVM.FName,
                        LName = SignUpVM.LName,
                        Age = SignUpVM.Age,
                    };
                    var result = await userManager.CreateAsync(AppUser,SignUpVM.Password);
                    if (result.Succeeded)
                    {
                        
                        return RedirectToAction("SignIn");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,"Error Occured within Registration");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "This Email Already Exist");
                }
            }
            return View(SignUpVM);
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel SignInVM)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(SignInVM.Email);
                if (user != null)
                {
                    var result = await signInMnager.PasswordSignInAsync(user, SignInVM.Password, true, false);
                    if (result.Succeeded)
                    {
                        TempData["UserName"] = user.UserName;
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Sign In");
                }
                else
                {
                    ModelState.AddModelError(string.Empty,"Not Valid Email");
                }
            }
            return View(SignInVM);
        }
        public IActionResult SignOut()
        {
            signInMnager.SignOutAsync();
            return View("SignIn");
        }
        public IActionResult ForgetPassword() { return View(); }
    }
}
