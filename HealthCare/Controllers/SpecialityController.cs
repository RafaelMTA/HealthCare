using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HealthCare.Areas.Identity.Data;
using HealthCare.Models;
using HealthCare.Abstraction;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HealthCare.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SpecialityController : Controller, ICreated, IUpdated
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public SpecialityController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Speciality
        public async Task<IActionResult> Index()
        {
            return _context.Specialities != null ?
                        View(await _context.Specialities.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Specialties'  is null.");
        }

        // GET: Speciality/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Specialities == null)
            {
                return NotFound();
            }

            var speciality = await _context.Specialities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (speciality == null)
            {
                return NotFound();
            }

            return View(speciality);
        }

        // GET: Speciality/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Speciality/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Speciality speciality)
        {
            if (ModelState.IsValid)
            {
                Created(speciality);
                _context.Add(speciality);              
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(speciality);
        }

        // GET: Speciality/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Specialities == null)
            {
                return NotFound();
            }

            var speciality = await _context.Specialities.FindAsync(id);
            if (speciality == null)
            {
                return NotFound();
            }
            return View(speciality);
        }

        // POST: Speciality/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Speciality speciality)
        {
            if (id != speciality.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Updated(speciality);
                    _context.Update(speciality);                   
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialityExists(speciality.Id))
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
            return View(speciality);
        }

        // GET: Speciality/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Specialities == null)
            {
                return NotFound();
            }

            var speciality = await _context.Specialities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (speciality == null)
            {
                return NotFound();
            }

            return View(speciality);
        }

        // POST: Speciality/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Specialities == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Specialties'  is null.");
            }
            var speciality = await _context.Specialities.FindAsync(id);
            if (speciality != null)
            {
                _context.Specialities.Remove(speciality);
            }

            var doctors = await _context.Doctors.Include(x => x.Speciality).Where(x => x.Speciality.Id == id).ToListAsync();

            _context.Doctors.RemoveRange(doctors);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialityExists(int id)
        {
            return (_context.Specialities?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private string GetUserId()
        {
            return _userManager.GetUserId(User)!;
        }

        public void Created(AuditableEntity model)
        {
            model.Created_At = DateTime.UtcNow;
            model.Created_By = GetUserId();
        }
        public void Updated(AuditableEntity model)
        {
            model.Updated_At = DateTime.UtcNow;
            model.Updated_By = GetUserId();
        }
    }
}
