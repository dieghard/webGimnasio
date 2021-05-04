using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webGimnasio.Data;
using webGimnasio.Models;

namespace webGimnasio.Controllers
{
    [Authorize]
    public class ClasesDiariasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ClasesDiariasController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        // GET: ClasesDiarias
        public IActionResult Index()
        {
            IEnumerable<ClasesDiarias> clases = null;

            if (_signInManager.Context.User.IsInRole("Administrador"))
            {
                clases = _context.ClasesDiarias.Include(c => c.Alumno).Include(c => c.Profesor);
            }
            else if (_signInManager.Context.User.IsInRole("Profesor"))
            {
                clases = _context.ClasesDiarias.Where(x => x.ProfesorID == User.FindFirst(ClaimTypes.NameIdentifier).Value).Include(c => c.Alumno).Include(c => c.Profesor);
            }
            else
            {
                clases = _context.ClasesDiarias.Where(x => x.AlumnoID == User.FindFirst(ClaimTypes.NameIdentifier).Value).Include(c => c.Alumno).Include(c => c.Profesor);
            }

            return View(clases.ToList());
        }

        // GET: ClasesDiarias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clasesDiarias = await _context.ClasesDiarias
                .Include(c => c.Alumno)
                .Include(c => c.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clasesDiarias == null)
            {
                return NotFound();
            }

            return View(clasesDiarias);
        }

        // GET: ClasesDiarias/Create
        public IActionResult Create()
        {
            string alumnoRolID = _context.Roles.FirstOrDefault(x => x.Name == "Alumno").Id;
            string profesorRolID = _context.Roles.FirstOrDefault(x => x.Name == "Profesor").Id;
            if (User.IsInRole("Alumno"))
            {
                ViewData["AlumnoID"] = new SelectList(_context.Users.Where(x => x.Id == User.FindFirst(ClaimTypes.NameIdentifier).Value), "Id", "NombreApellido");
             
            }
            else { 
                ViewData["AlumnoID"]   = new SelectList(_context.Users.Where(X => X.Roles.Any(r => r.RoleId == alumnoRolID  )), "Id", "NombreApellido");
            }
            ViewData["ProfesorID"] = new SelectList(_context.Users.Where(X => X.Roles.Any(r => r.RoleId == profesorRolID)), "Id", "NombreApellido");
            ViewData["ClaseID"]    = new SelectList(_context.Clase, "Id", "Descripcion");
            
            return View();
        }

        // POST: ClasesDiarias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProfesorID,AlumnoID,ClaseID,DiaHora,Observaciones")] ClasesDiarias clasesDiarias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clasesDiarias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            string alumnoRolID = _context.Roles.FirstOrDefault(x => x.Name == "Alumno").Id;
            string profesorRolID = _context.Roles.FirstOrDefault(x => x.Name == "Profesor").Id;

            ViewData["AlumnoID"] = new SelectList(_context.Users.Where(X => X.Roles.Any(r => r.RoleId == alumnoRolID)), "Id", "NombreApellido");
            ViewData["ProfesorID"] = new SelectList(_context.Users.Where(X => X.Roles.Any(r => r.RoleId == profesorRolID)), "Id", "NombreApellido");
            ViewData["ClaseID"] = new SelectList(_context.Clase, "Id", "Descripcion");
            
            return View(clasesDiarias);
        }

        // GET: ClasesDiarias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clasesDiarias = await _context.ClasesDiarias.FindAsync(id);
            if (clasesDiarias == null)
            {
                return NotFound();
            }
            string alumnoRolID = _context.Roles.FirstOrDefault(x => x.Name == "Alumno").Id;
            string profesorRolID = _context.Roles.FirstOrDefault(x => x.Name == "Profesor").Id;

            ViewData["AlumnoID"] = new SelectList(_context.Users.Where(X => X.Roles.Any(r => r.RoleId == alumnoRolID)), "Id", "NombreApellido");
            ViewData["ProfesorID"] = new SelectList(_context.Users.Where(X => X.Roles.Any(r => r.RoleId == profesorRolID)), "Id", "NombreApellido");
            ViewData["ClaseID"] = new SelectList(_context.Clase, "Id", "Descripcion");

            return View(clasesDiarias);
        }

        // POST: ClasesDiarias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfesorID,AlumnoID,ClaseID,DiaHora,Observaciones")] ClasesDiarias clasesDiarias)
        {
            if (id != clasesDiarias.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clasesDiarias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClasesDiariasExists(clasesDiarias.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            string alumnoRolID = _context.Roles.FirstOrDefault(x => x.Name == "Alumno").Id;
            string profesorRolID = _context.Roles.FirstOrDefault(x => x.Name == "Profesor").Id;

            ViewData["AlumnoID"] = new SelectList(_context.Users.Where(X => X.Roles.Any(r => r.RoleId == alumnoRolID)), "Id", "NombreApellido");
            ViewData["ProfesorID"] = new SelectList(_context.Users.Where(X => X.Roles.Any(r => r.RoleId == profesorRolID)), "Id", "NombreApellido");
            ViewData["ClaseID"] = new SelectList(_context.Clase, "Id", "Descripcion");

            return View(clasesDiarias);
        }

        // GET: ClasesDiarias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clasesDiarias = await _context.ClasesDiarias
                .Include(c => c.Alumno)
                .Include(c => c.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clasesDiarias == null)
            {
                return NotFound();
            }

            return View(clasesDiarias);
        }

        // POST: ClasesDiarias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clasesDiarias = await _context.ClasesDiarias.FindAsync(id);
            _context.ClasesDiarias.Remove(clasesDiarias);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClasesDiariasExists(int id)
        {
            return _context.ClasesDiarias.Any(e => e.Id == id);
        }
    }
}
