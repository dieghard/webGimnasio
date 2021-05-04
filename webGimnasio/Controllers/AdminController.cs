using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webGimnasio.Data;
using webGimnasio.Models;

namespace webGimnasio.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            ViewBag.Roles = _roleManager.Roles.ToList();
            var usuarios = _context.Users.Include("Roles").ToList();

            return View(usuarios);
        }

        public IActionResult Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.Users.Where(x=> x.Id == id).Include("Roles").FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            user.RolID = user.Roles.FirstOrDefault()?.RoleId;

            ViewData["Roles"] = new SelectList(_context.Roles, "Id", "Name"); 
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,RolID")] ApplicationUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dbRoles = _context.Roles.ToList();
                    var dbUser = _context.Users.Where(x => x.Id == id).Include("Roles").FirstOrDefault();

                    if (dbUser.Roles.Count != 0)
                    {
                        await _userManager.RemoveFromRoleAsync(dbUser, dbRoles.FirstOrDefault(x => x.Id == dbUser.Roles.First().RoleId).Name);
                    }

                    await _userManager.AddToRoleAsync(dbUser, dbRoles.FirstOrDefault(x => x.Id == user.RolID).Name);

                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }


    }
}
