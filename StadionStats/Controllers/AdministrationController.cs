using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using StadionStats.Models;
using StadionStats.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace StadionStats.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }


        // List of users
        public async Task<IActionResult> Brugere()
        {
            return View(await _userManager.Users.ToListAsync());
        }



        // Edit user
        [HttpGet]
        public async Task<IActionResult> RedigerBruger(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Brugeren med ID = {id} kan ikke findes.";
                return View("NotFound");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new RedigerBrugerViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = userRoles
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> RedigerBruger(RedigerBrugerViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Brugeren med Id = {model.Id} kan ikke findes.";
                return View("NotFound");
            }
            else
            {
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Brugere");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);

            }

        }


        // Delete user

        public async Task<IActionResult> SletBruger(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Bruger med Id = {id} kan ikke findes.";
                return View("NotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Brugere");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("Brugere");
            }

        }


        // Edit role on user

        [HttpGet]
        public async Task<IActionResult> AdminBrugerRoller(string userId)
        {
            ViewBag.userId = userId;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Bruger med Id = {userId} kan ikke findes.";
                return View("NotFound");
            }

            var model = new List<BrugerRollerViewModel>();

            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new BrugerRollerViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }

                model.Add(userRolesViewModel);

            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AdminBrugerRoller(List<BrugerRollerViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Bruger med Id = {userId} kan ikke findes.";
                return View("NotFound");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Kan ikke slette");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Kan ikke tilføje");
                return View(model);
            }


            return RedirectToAction("RedigerBruger", new { Id = userId });

        }


        // GET: Roles
        public async Task<IActionResult> Roller()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }


        // GET: Create roles

        [HttpGet]
        public IActionResult OpretRolle()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> OpretRolle(OpretRolleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Roller");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }


        // Edit role

        [HttpGet]
        public async Task<IActionResult> RedigerRolle(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rollen med Id = {id} kan ikke findes.";
                return View("NotFound");
            }

            var model = new RedigerRollerViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);


        }


        [HttpPost]
        public async Task<IActionResult> RedigerRolle(RedigerRollerViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rollen med Id = {model.Id} kan ikke findes.";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Roller");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);

            }

        }

        // Edit users on role

        [HttpGet]
        public async Task<IActionResult> AdminRolleBrugere(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Brugeren med Id = {roleId} kan ikke findes.";
                return View("NotFound");
            }

            var model = new List<BrugerRolleViewModel>();

            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new BrugerRolleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AdminRolleBrugere(List<BrugerRolleViewModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Brugeren med Id = {roleId} kan ikke findes.";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("RedigerRolle", new { Id = roleId });
                }
            }

            return RedirectToAction("RedigerRolle", new { Id = roleId });
        }
    }
}
