using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthCare.Areas.Identity.Data;
using HealthCare.Models;
using HealthCare.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HealthCare.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppointmentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Appointments
        [Authorize(Roles = "Admin, Patient")]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var applicationDbContext = _context.Appointments.Include(a => a.Clinic).Include(a => a.Doctor).Include(x => x.Doctor.Speciality).Include(a => a.User);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                string userId = GetUserId();

                if (userId == null) return View();

                var applicationDbContext = _context.Appointments.Include(a => a.Clinic).Include(a => a.Doctor).Include(x => x.Doctor.Speciality).Where(u => u.UserId == userId);
                return View(await applicationDbContext.ToListAsync());
            }             
        }

        // GET: Appointments/Details/5
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Clinic)
                .Include(a => a.Doctor)
                .Include(a => a.Doctor.Speciality)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        [Authorize(Roles = "Patient")]
        public IActionResult Create()
        {
            ViewData["ClinicId"] = new SelectList(_context.Clinics, "Id", "Name");          
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Create([Bind("Date,ClinicId,DoctorId,Id")] Appointment appointment)
        {
            string id = GetUserId();

            if (id == null) return View(appointment);

            appointment.UserId = id;

            var appointments = _context.Appointments
                .Include(x => x.Clinic)
                .Include(x => x.Doctor)
                .Where(x => x.ClinicId == appointment.ClinicId && x.DoctorId == appointment.DoctorId)
                .ToList();

            if(CheckIfAppointmentDateExists(appointments, appointment))
            {
                TempData["Error"] = "An appointment is already schedule for that period";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClinicId"] = new SelectList(_context.Clinics, "Id", "Name", appointment.ClinicId);
           
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            ViewData["ClinicId"] = new SelectList(_context.Clinics, "Id", "Name", appointment.ClinicId);
            ViewData["DoctorId"] = appointment.DoctorId;

            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Edit(int id, [Bind("Date,ClinicId,DoctorId,Id")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            appointment.UserId = GetUserId();

            var appointments = _context.Appointments
                .Include(x => x.Clinic)
                .Include(x => x.Doctor)
                .Where(x => 
                x.ClinicId == appointment.ClinicId 
                && 
                x.DoctorId == appointment.DoctorId
                &&
                x.Id != id
                )
                .ToList();

            if (CheckIfAppointmentDateExists(appointments, appointment))
            {
                TempData["Error"] = "An appointment is already schedule for that period";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
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

            ViewData["ClinicId"] = new SelectList(_context.Clinics, "Id", "Name", appointment.ClinicId);
            ViewData["DoctorId"] = appointment.DoctorId;

            return View(appointment);
        }

        // GET: Appointments/Delete/5
        [Authorize(Roles = "Admin, Patient")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Clinic)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Patient")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Appointment'  is null.");
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public JsonResult getDoctors(int id, int? doctorId)
        {
            var selectList = new List<SelectListItem>();

            var doctors = _context.ClinicDoctors.Include(x => x.Doctor).Include(x => x.Doctor.Speciality).Where(e => e.ClinicId == id).Select(e => e.Doctor).ToList();

            foreach (var doctor in doctors)
            {
                SelectListItem item = new SelectListItem();

                item.Text = doctor!.FullName + " - " + doctor!.Speciality.Name;
                item.Value = doctor!.Id.ToString();

                if (doctorId == doctor!.Id) item.Selected = true;

                selectList.Add(item);          
            }

            if(doctorId != 0)
            {
                return Json(new SelectList(selectList, "Value", "Text", doctorId));
            }
            else
            {
                return Json(new SelectList(selectList, "Value", "Text", System.Web.Mvc.JsonRequestBehavior.AllowGet));
            }        
        }

        private bool AppointmentExists(int id)
        {
          return (_context.Appointments?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private string GetUserId()
        {
            return _userManager.GetUserId(User)!;
        }

        private bool AppointmentAvailable(int clinicId, int doctorId)
        {
            var appointment = _context.Appointments.FirstOrDefault(x => x.ClinicId == clinicId && x.DoctorId == doctorId);
            if (appointment == null) return true;
            return false;
        }

        private bool CheckIfAppointmentDateExists(List<Appointment> appointments, Appointment appointment)
        {
            foreach (var item in appointments)
            {
                if (item.Date.Date == appointment.Date.Date)
                {
                    if (item.Date.Hour == appointment.Date.Hour)
                    {            
                        return true;
                    }
                }               
            }
            return false;
        }

        private async Task<ICollection<string>> GetUserRoles()
        {
            var user = await _userManager.FindByIdAsync(GetUserId());
            return await _userManager.GetRolesAsync(user!);
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
