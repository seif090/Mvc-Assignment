using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using company.web.Models; // If you have a folder where models are stored
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace company.web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // GET: Role
        public async Task<IActionResult> Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (!string.IsNullOrWhiteSpace(roleName))
            {
                var roleExists = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error creating role.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Role already exists.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Role name cannot be empty.");
            }
            return View();
        }

        // GET: Role/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    return View(role);
                }
            }
            return RedirectToAction("Index");
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error deleting role.");
                }
            }
            return View();
        }
    }
}
