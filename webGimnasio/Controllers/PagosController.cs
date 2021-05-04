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
    public class PagosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PagosController(ApplicationDbContext context)
        {
            _context = context;
        }
   
        // GET: Pagos
        public IActionResult Index()
        {
            IEnumerable<Pagos> clases = null;
            
            if (User.IsInRole("Administrador"))
            {
                clases = _context.Pagos.Include(c => c.Alumno);
            }
            if (User.IsInRole("Alumno"))
            {
                clases = _context.Pagos.Where(x => x.AlumnoID == User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            
            return View(clases.ToList());
        }

        // GET: Pagos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pagos = await _context.Pagos
                .Include(p => p.Alumno)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pagos == null)
            {
                return NotFound();
            }

            return View(pagos);
        }

        // GET: Pagos/Create
        public IActionResult Create()
        {
            string roleID = _context.Roles.FirstOrDefault(r => r.Name == "Alumno").Id;
            Pagos pagos = new Pagos()
            {
                Fecha = DateTime.Now.Date
            };
            return View(null, pagos);
        }

        // POST: Pagos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,AlumnoID,Importe,Observaciones")] Pagos pagos)
        {
            if (ModelState.IsValid)
            {
                if (_context.Pagos.Any(p => p.AlumnoID == pagos.AlumnoID && p.Fecha.Year == pagos.Fecha.Year && p.Fecha.Month == pagos.Fecha.Month))
                {
                    ModelState.AddModelError("PagosError", "El alumno ya pagó para esta mes");
                }
                else {
                    _context.Add(pagos);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            string roleID = _context.Roles.FirstOrDefault(r => r.Name == "Alumno").Id;

            ViewData["AlumnoID"] = new SelectList(_context.Users.Where(u => u.Roles.Any(r => r.RoleId == roleID)), "Id", "NombreApellido");
            return View(pagos);
        }

        // GET: Pagos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pagos = await _context.Pagos.FindAsync(id);
            if (pagos == null)
            {
                return NotFound();
            }

            string roleID = _context.Roles.FirstOrDefault(r => r.Name == "Alumno").Id;

            ViewData["AlumnoID"] = new SelectList(_context.Users.Where(u => u.Roles.Any(r => r.RoleId == roleID)), "Id", "NombreApellido");

            return View(pagos);
        }

        // POST: Pagos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,AlumnoID,Importe,Observaciones")] Pagos pagos)
        {
            if (id != pagos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pagos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagosExists(pagos.Id))
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
            string roleID = _context.Roles.FirstOrDefault(r => r.Name == "Alumno").Id;

            ViewData["AlumnoID"] = new SelectList(_context.Users.Where(u => u.Roles.Any(r => r.RoleId == roleID)), "Id", "NombreApellido");
            return View(pagos);
        }

        // GET: Pagos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pagos = await _context.Pagos
                .Include(p => p.Alumno)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pagos == null)
            {
                return NotFound();
            }

            return View(pagos);
        }

        // POST: Pagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pagos = await _context.Pagos.FindAsync(id);
            _context.Pagos.Remove(pagos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagosExists(int id)
        {
            return _context.Pagos.Any(e => e.Id == id);
        }
    }
}

