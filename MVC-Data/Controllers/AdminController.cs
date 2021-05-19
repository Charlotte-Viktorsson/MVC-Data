using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Data.Models.Data;
using MVC_Data.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            UserViewModel model = new UserViewModel();
            model.Users = _userManager.Users.ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            if (!User.IsInRole("SuperAdmin") && (userRoles.Contains("Admin") || userRoles.Contains("SuperAdmin")))
            {
                //admins får bara hanteras av SuperAdmin
                return RedirectToAction(nameof(Index));
            }
            var model = new EditUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                BirthDay = user.BirthDate,
                Roles = userRoles,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ModelState.AddModelError("", "$User with Id = {model.Id} cannot be found");
                return View(model);
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.BirthDate = model.BirthDay;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                //not allowed to remove default users Admin or SuperAdmin
                if (user.UserName == "Admin" || user.UserName == "SuperAdmin")
                {
                    return RedirectToAction(nameof(Index));
                }

                //verify it is not an user with admin rights
                var userRoles = await _userManager.GetRolesAsync(user);
                if (!User.IsInRole("SuperAdmin") && (userRoles.Contains("Admin") || userRoles.Contains("SuperAdmin")))
                {

                }
                //check if user has a role
                var roles = await _userManager.GetRolesAsync(user);

                var deleteRolesResult = await _userManager.RemoveFromRolesAsync(user, roles);

                if (deleteRolesResult.Succeeded)
                {
                    var deleteUserResult = await _userManager.DeleteAsync(user);
                    if (deleteUserResult.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string id)
        {

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }

            List<IdentityRole> allRoles = _roleManager.Roles.ToList();
            IList<string> usersRoles = await _userManager.GetRolesAsync(user);
            List<string> notAssignedRoles = new List<string>();
            foreach (var role in allRoles)
            {
                bool result = await _userManager.IsInRoleAsync(user, role.Name);
                if (result == false)
                {
                    notAssignedRoles.Add(role.Name);
                }
            }

            ManageUserRolesViewModel model = new ManageUserRolesViewModel
            {
                NotAssignedRoles = notAssignedRoles,
                Roles = usersRoles,
                UserId = user.Id,
                UserName = user.UserName
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddRoleToUser(string uName, string rName)
        {

            var user = await _userManager.FindByNameAsync(uName);
            if (user != null)
            {
                var assignResult = await _userManager.AddToRoleAsync(user, rName);
                if (assignResult.Succeeded)
                {
                    return RedirectToAction(nameof(ManageUserRoles), new { id = user.Id });
                }
            }
            else
            {
                ViewBag.ErrorMessage = $"User with UserName = {uName} cannot be found";
                return View("NotFound");
            }
            return RedirectToAction(nameof(ManageUserRoles), new { id = user.Id });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveRoleFromUser(string uName, string rName)
        {
            var user = await _userManager.FindByNameAsync(uName);
            if (user != null)
            {
                await _userManager.RemoveFromRoleAsync(user, rName);
            }
            else
            {
                ViewBag.ErrorMessage = $"User with UserName = {uName} cannot be found";
                return View("NotFound");
            }
            return RedirectToAction(nameof(ManageUserRoles), new { id = user.Id });
        }
    }
}
