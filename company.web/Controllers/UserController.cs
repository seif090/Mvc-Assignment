using Company.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace company.web.Controllers
{
    //[Authorize(Roles="Admin")]
    public class UserController : Controller
    {

        public UserController(UserManager<ApplicationUser> userManger)
        {
            _UserManger = userManger;
        }

        public UserManager<ApplicationUser> _UserManger { get; }

        public async Task<IActionResult> Index(string searchInput)
        {
            List<ApplicationUser> users;
            if (string.IsNullOrEmpty(searchInput))
            {
                users = await _UserManger.Users.ToListAsync();
            }
            else
            {
                users =await _UserManger.Users.Where(user=>  user.NormalizedEmail.Trim().ToUpper() == searchInput.Trim().ToUpper()).ToListAsync();
            }
            return View(users);
        }
    }
}
