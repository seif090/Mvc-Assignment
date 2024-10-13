using company.web.Models;
using Company.Data.Entities;
using Company.Services.Helper;
using Company.Data.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace company.web.Controllers
{
    public class AccountController : Controller
    {

        public UserManager<ApplicationUser> _UserManger { get; }
        public SignInManager<ApplicationUser> _SignInManager { get; }

        public AccountController(UserManager<ApplicationUser> userManger,
            SignInManager<ApplicationUser> signInManager)
        {
            _UserManger = userManger;
            _SignInManager = signInManager;
        }

        #region Signup

        #endregion
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = input.Email.Split("@")[0],
                    Email = input.Email,
                    FirstName = input.FirstName,
                    LastName = input.LastName,

                };
                var result = await _UserManger.CreateAsync(user, input.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel input)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        var user = await _UserManger.FindByEmailAsync(input.Email);
        //        if (user is not null)
        //        {
        //            if (await _UserManger.CheckPasswordAsync(user, input.Password))
        //            {
        //                var result = await _SignInManager.PasswordSignInAsync(user, input.Password, true, true);
        //                if (result.Succeeded)
        //                    return RedirectToAction("Index", "Home");
        //            }
        //        }

        //        ModelState.AddModelError("", "Incorrect Email or password");
        //        return View(input);
        //    }
        //    return View(input);
        //}

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManger.FindByEmailAsync(input.Email);

                if (user != null)
                {
                    // Sign in the user (this will check the password for you)
                    var result = await _SignInManager.PasswordSignInAsync(user.UserName, input.Password, true, lockoutOnFailure: true);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Your account has been locked out. Please try again later.");
                    }
                    else if (result.IsNotAllowed)
                    {
                        ModelState.AddModelError("", "Your account is not allowed to sign in at this time.");
                    }
                }

                // If user is null or login failed
                ModelState.AddModelError("", "Incorrect email or password.");
            }

            return View(input); // Return to login page with input for the user to try again
        }

        public async Task<IActionResult> SignOut()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManger.FindByEmailAsync(input.Email);

                if (user != null)
                {

                    var token = await _UserManger.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("ResetPassword", "Account", new { email = input.Email, Token = token }, Request.Scheme);

                    var email = new Email
                    {
                        Body = url,
                        Subject = "Reset password",
                        To = input.Email,

                    };
                    EmailSettings.SendEmail(email);

                }

                // If user is null or login failed
                ModelState.AddModelError("", "Incorrect email or password.");
            }

            return View(input); // Return to
        }

        public IActionResult CheckYourInput()
        {
            return View();
        }   
        
        public IActionResult ResetPassword(string Email,string Token) 
        { 
        
        
            return View();
        }


        [HttpPost]
        public async Task< IActionResult> ResetPassword(ResetPasswordViewModel input)
        {

            if (ModelState.IsValid)
            {
                var user = await _UserManger.FindByEmailAsync(input.Email);

                if (user != null)
                {
                    var result = await _UserManger.ResetPasswordAsync(user, input.Token,input.Password);
                    return View();
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }

                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }
                    return View();
        }
        }

    

}
