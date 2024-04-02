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

		public AccountController(UserManager<ApplicationUser> userManager)
        {
			_userManager = userManager;
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

        public IActionResult SignIn(SignupViewModel model) 
        {
			return View(model);
		}
    }
}
