using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthCare.Areas.Identity.Data;
using HealthCare.Models;
using Microsoft.AspNetCore.Authorization;

namespace HealthCare.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClinicDoctorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClinicDoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClinicDoctor
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClinicDoctors.Include(c => c.Clinic).Include(c => c.Doctor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClinicDoctor/Create
        public IActionResult Create()
        {
            ViewData["Clinic"] = new SelectList(_context.Clinics, "Id", "Name");
            ViewData["Doctor"] = new SelectList(_context.Doctors, "Id", "FullName");
            return View();
        }

        // POST: ClinicDoctor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClinicId,DoctorId")] ClinicDoctor clinicDoctor)
        {
            var exists = await _context.ClinicDoctors.AnyAsync(x => x.ClinicId == clinicDoctor.ClinicId && x.DoctorId == clinicDoctor.DoctorId);

            if (exists)
            {
                TempData["Error"] = "You already have the doctor assigned for that clinic";
                return RedirectToAction(nameof(Index));
            }

            ViewData["Clinic"] = new SelectList(_context.Clinics, "Id", "Name", clinicDoctor.ClinicId);
            ViewData["Doctor"] = new SelectList(_context.Doctors, "Id", "FullName", clinicDoctor.DoctorId);

            if (clinicDoctor.ClinicId == null || clinicDoctor.DoctorId == null) return View(clinicDoctor);

            _context.ClinicDoctors.Add(clinicDoctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ClinicDoctor/Delete/5
        public async Task<IActionResult> Delete(int? doctorId, int? clinicId)
        {
            if (doctorId == null || clinicId == null || _context.ClinicDoctors == null)
            {
                return NotFound();
            }

            var clinicDoctor = await _context.ClinicDoctors
                .Include(c => c.Clinic)
                .Include(c => c.Doctor)
                .FirstOrDefaultAsync(m => m.ClinicId == clinicId && m.DoctorId == doctorId);

            if (clinicDoctor == null)
            {
                return NotFound();
            }

            return View(clinicDoctor);
        }

        // POST: ClinicDoctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? clinicId, int? doctorId)
        {
            if (_context.ClinicDoctors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ClinicDoctors'  is null.");
            }
            var clinicDoctor = await _context.ClinicDoctors.Where(x => x.ClinicId == clinicId && x.DoctorId == doctorId).FirstOrDefaultAsync();

            if (clinicDoctor != null)
            {
                _context.ClinicDoctors.Remove(clinicDoctor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClinicDoctorExists(int? clinicId, int? doctorId)
        {
            return (_context.ClinicDoctors?.Any(e => e.ClinicId == clinicId && e.DoctorId == doctorId)).GetValueOrDefault();
        }
    }
}
