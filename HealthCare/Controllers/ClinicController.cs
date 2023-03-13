using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthCare.Areas.Identity.Data;
using HealthCare.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HealthCare.Controllers
{
    [Authorize]
    public class ClinicController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClinicController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clinic
        [Authorize(Roles = "Admin, Patient")]
        public async Task<IActionResult> Index()
        {
              return _context.Clinics != null ? 
                          View(await _context.Clinics.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Clinics'  is null.");
        }

        // GET: Clinic/Details/5
        [Authorize(Roles = "Admin, Patient")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clinics == null)
            {
                return NotFound();
            }

            var clinic = await _context.Clinics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clinic == null)
            {
                return NotFound();
            }

            return View(clinic);
        }

        // GET: Clinic/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clinic/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Name,Id,Address")] Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clinic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clinic);
        }

        // GET: Clinic/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clinics == null)
            {
                return NotFound();
            }

            var clinic = await _context.Clinics.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);
            if (clinic == null)
            {
                return NotFound();
            }
            return View(clinic);
        }

        // POST: Clinic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id,Address")] Clinic clinic)
        {
            if (id != clinic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clinic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClinicExists(clinic.Id))
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
            return View(clinic);
        }

        // GET: Clinic/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clinics == null)
            {
                return NotFound();
            }

            var clinic = await _context.Clinics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clinic == null)
            {
                return NotFound();
            }

            return View(clinic);
        }

        // POST: Clinic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clinics == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Clinics'  is null.");
            }
            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic != null)
            {
                _context.Clinics.Remove(clinic);
            }

            var clinicDoctors = await _context.ClinicDoctors.Include(x => x.Clinic).Where(x => x.Clinic.Id == id).ToListAsync();

            _context.ClinicDoctors.RemoveRange(clinicDoctors);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClinicExists(int id)
        {
          return (_context.Clinics?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
