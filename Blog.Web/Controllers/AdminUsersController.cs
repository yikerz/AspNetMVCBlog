/* 280. Controllers for admin to manage users */
using Blog.Web.Models.View;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    /* 291. Add authorization to admin only */
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        /* 286. Create constructor taking userRepo */
        private readonly IUserRepository userRepo;
        public AdminUsersController(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }
        /* 281. Create List action method (GET) */
        [HttpGet]
        public async Task<IActionResult> List()
        {
            /* 288. Implement List action (GET) */
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
    }
}
