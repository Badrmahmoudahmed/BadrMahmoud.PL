using BadrMahmoud.PL.Models.User;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BadrMahmoud.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignupViewModel model) 
        {
            
            if (ModelState.IsValid) 
            {
				var userEmail = await _userManager.FindByEmailAsync(model.Email);
                if (userEmail is null) 
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        FName = model.FName,
                        LName = model.LName,
                        IsAgree = model.IsAgree,
                    };

                   var result =  await _userManager.CreateAsync(user , model.Passoword);
                    if (result.Succeeded) 
                    {
                        return RedirectToAction(nameof(SignIn));
                    }
                    else
                    {
                        foreach(var error in result.Errors)
							ModelState.AddModelError(string.Empty, error.Description);
					}
                };
                ModelState.AddModelError(string.Empty, "This user Is Already Exist");
            }
            return View(model);
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SigninViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Passoword);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Passoword, model.RememberMe, false);

                        if (result.Succeeded)
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        if (result.IsNotAllowed)
                            ModelState.AddModelError(string.Empty, "Ur Account Not Confirmed Yet");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invaild Login");


            }
            return View(model);
        }
        public async new Task<ActionResult> SignOut()
        {
             await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn), "Account");
        }
    }
}
