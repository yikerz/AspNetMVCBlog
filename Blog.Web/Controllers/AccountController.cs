using Blog.Web.Models.View;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AccountController : Controller
    {
        /* 181. Create constructor taking UserManager */
        private readonly UserManager<IdentityUser> userManager;
        public AccountController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        /* 175. Create Register action method (GET) */
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        /* 177. Create Register action method (POST) */
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            /* 182. Instantiate IdentityUser */
            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email,
            };
            /* 183. Create account using userManager */
            var createUserResponse = await userManager.CreateAsync(identityUser, registerViewModel.Password);
            /* 184. Add role to user */
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

    }
}
