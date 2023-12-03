using Blog.Web.Models.View;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email,
            };
            var createUserResponse = await userManager.CreateAsync(identityUser, registerViewModel.Password);
            if (createUserResponse.Succeeded)
            {
                var addRoleResponse = await userManager.AddToRoleAsync(identityUser, "User");
                if (addRoleResponse.Succeeded)
                {
                    return RedirectToAction("Register");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var signInResponse = await signInManager.PasswordSignInAsync(loginViewModel.Username, 
                                                    loginViewModel.Password, 
                                                    false, false);
            if (signInResponse != null && signInResponse.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            /* 200. Logout */
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
