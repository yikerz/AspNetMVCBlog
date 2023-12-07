using Blog.Web.Models.View;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository userRepo;
        private readonly UserManager<IdentityUser> userManager;
        public AdminUsersController(IUserRepository userRepo, UserManager<IdentityUser> userManager)
        {
            this.userRepo = userRepo;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await userRepo.GetAll();

            var usersViewModel = new UserViewModel();
            usersViewModel.Users = new List<User>();
            foreach (var user in users)
            {
               usersViewModel.Users.Add(new User { 
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    EmailAddress = user.Email,
               });
            }

            return View(usersViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> List(UserViewModel request)
        {
            var identityUser = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email,
            };
            var identityResult = await userManager.CreateAsync(identityUser, request.Password);
            if (identityResult != null)
            {
                if (identityResult.Succeeded)
                {
                    var roles = new List<string> { "User" };
                    if (request.AdminRoleCheckbox)
                    {
                        roles.Add("Admin");
                    }

                    identityResult = await userManager.AddToRolesAsync(identityUser, roles);

                    if (identityResult != null && identityResult.Succeeded)
                    {
                        return RedirectToAction("List", "AdminUsers");
                    }
                }
            }
            return View();
        }
        /* 304. Create and implement Delete action method (POST) */
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                var identityResult = await userManager.DeleteAsync(user);
                if (identityResult != null && identityResult.Succeeded)
                {
                    return RedirectToAction("List", "AdminUsers");
                }
            }

            return View();
        }
    }
}
