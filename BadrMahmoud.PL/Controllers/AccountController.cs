using BadrMahmoud.PL.Models.User;
using BadrMahmoud.PL.Services.EmailSender;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BadrMahmoud.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration,IEmailSender emailSender)
        {
			_userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailSender = emailSender;
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
        public async new Task<IActionResult> SignOut()
        {
             await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn), "Account");
        }

        public ActionResult SendResetPasswordEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordEmail(SendResetPasswordEmilViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                
                var user = await _userManager.FindByEmailAsync(model.Email);
                var resetpasswordtoken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetpassworurl = Url.Action("ResetPassword", "Account", new { email = model.Email , token =resetpasswordtoken }, "https", "localhost:44320");
                if (user is not null)
                {
                    //await _emailSender.SendAsync("badrmahmoud201312123@gmail.com", model.Email, "Reset UR Password", resetpassworurl);
                    await _emailSender.SendAsync("badrmahmoud201312@gmail.com", model.Email, "Reset Password", resetpassworurl);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                ModelState.AddModelError(string.Empty, "Invaild Email");
            }
            return View(model);
        }

        public ActionResult ResetPassword(string email , string token)
        {
            TempData["Email"] = email;
            TempData["Token"] = token;
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
            if (ModelState.IsValid)
            {
                var email = TempData["Email"] as string;
                var token = TempData["token"] as string;
                var user = await _userManager.FindByEmailAsync(email);
                await _userManager.ResetPasswordAsync(user, token, model.ConfirmPassword);
				return RedirectToAction(nameof(SignIn));

			}
            return View(model);
		}

        public IActionResult CheckYourInbox()
        {
            return View();
        }
    }
}
