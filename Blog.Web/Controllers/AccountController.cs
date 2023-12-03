using Blog.Web.Models.View;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AccountController : Controller
    {
        /* 194. Add SignInManager to the constructor */
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
        /* 188. Create Login action method (GET) */
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        /* 193. Create Login action method (POST) */
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            /* 195. Sign In */
            var signInResponse = await signInManager.PasswordSignInAsync(loginViewModel.Username, 
                                                    loginViewModel.Password, 
                                                    false, false);
            if (signInResponse != null && signInResponse.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }
    }
}
