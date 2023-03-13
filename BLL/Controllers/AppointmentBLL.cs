using Microsoft.EntityFrameworkCore;
using HealthCare.Areas.Identity.Data;
using HealthCare.Models;
using HealthCare.Abstraction;
using Microsoft.AspNetCore.Identity;

namespace HealthCare.Controllers
{
    public class AppointmentBLL
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppointmentBLL(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //Return All Appointments
        public ICollection<Appointment> GetAll()
        {
            return _context.Appointments.Include(x => x.Clinic).Include(x => x.User).ToList();
        }

        public Appointment Get(int? id)
        {
            return _context.Appointments.Include(x => x.Clinic).Include(x => x.User).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Create(Appointment appointment)
        {
            if (appointment == null) return false;

            try
            {
                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        } 

        public async Task<bool> Update(int id, Appointment appointment)
        {
            if (id != appointment.Id) return false;

            try
            {
                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }          
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var appointment = _context.Appointments.Where(x => x.Id == id).FirstOrDefault();
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        private bool AppointmentExists(int id)
        {
          return (_context.Appointments?.Any(e => e.Id == id)).GetValueOrDefault();
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
    }
}
