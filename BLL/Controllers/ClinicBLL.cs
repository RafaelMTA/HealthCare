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
    public class ClinicBLL
    {
        private readonly ApplicationDbContext _context;

        public ClinicBLL(ApplicationDbContext context)
        {
            _context = context;
        }

        

        private bool ClinicExists(int id)
        {
          return (_context.Clinics?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
