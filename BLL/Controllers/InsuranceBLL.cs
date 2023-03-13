using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthCare.Areas.Identity.Data;
using HealthCare.Models;
using Microsoft.AspNetCore.Identity;
using HealthCare.Abstraction;
using Microsoft.AspNetCore.Authorization;
using HealthCare.ValueObjects;

namespace HealthCare.Controllers
{
    [Authorize]
    public class InsuranceBLL : Controller, ICreated, IUpdated
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public InsuranceBLL(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Insurance
        [Authorize(Roles = "Admin, Patient")]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                return _context.Insurances != null ?
                          View(await _context.Insurances.Include(i => i.Product).Include(i => i.User).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Insurances'  is null.");
            }
            else
            {
                string userId = GetUserId();

                if (userId == null) return View();

                return _context.Insurances != null ?
                            View(await _context.Insurances.Where(u => u.UserId == userId).Include(i => i.Product).ToListAsync()) :
                            Problem("Entity set 'ApplicationDbContext.Insurances'  is null.");
            }             
        }  

        // GET: Insurance/Details/5
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Insurances == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurances
                .Include(x => x.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        // GET: Insurance/Create
        [Authorize(Roles = "Patient")]
        public IActionResult Create(int? id)
        {
            var product = _context.Products.Where(x => x.Id == id).FirstOrDefault();
            if(product != null) ViewBag.Product = product;

            return View();
        }

        // POST: Insurance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Create([Bind("Start,End,UserId,Product,ProductId")] Insurance insurance, int? id)
        {
            string userId = GetUserId();

            if (id == null) return View(insurance);

            insurance.UserId = userId;
            insurance.ProductId = id;

            var insurances = await _context.Insurances.Where(x => x.UserId == GetUserId()).ToListAsync();

            if (CheckInsuranceTypeExists(insurance.ProductId) && CheckOverlap(insurance.Start, insurance.End, insurances)) 
            {
                TempData["Error"] = "You already have an Insurance for that period";
                return RedirectToAction(nameof(Index)); 
            }

            if (ModelState.IsValid)
            {
                _context.Add(insurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insurance);
        }

        // GET: Insurance/Edit/5
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Insurances == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurances.Where(x => x.Id == id).Include(p => p.Product).Include(u => u.User).FirstOrDefaultAsync();

            if (insurance == null)
            {
                return NotFound();
            }
            return View(insurance);
        }

        // POST: Insurance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Edit(int id, [Bind("Start,End,User,Product,Id")] Insurance insurance, int? productId)
        {
            var userId = GetUserId();

            if (id != insurance.Id)
            {
                return NotFound();
            }

            var insurances = await _context.Insurances.Where(x => x.UserId == GetUserId() && x.Id != id).ToListAsync();
            
            var existInsurance = await _context.Insurances.Where(x => x.Id == id).Include(x => x.User).Include(x => x.Product).FirstOrDefaultAsync();

            if (CheckInsuranceTypeExists(existInsurance.Product.Id) && CheckOverlap(insurance.Start, insurance.End, insurances)) return View(insurance);
            
            existInsurance.Start = insurance.Start;
            existInsurance.End = insurance.End; 

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(existInsurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceExists(insurance.Id))
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
            return View(insurance);
        }

        // GET: Insurance/Delete/5
        [Authorize(Roles = "Admin, Patient")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Insurances == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        // POST: Insurance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Patient")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Insurances == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Insurances'  is null.");
            }
            var insurance = await _context.Insurances.FindAsync(id);
            if (insurance != null)
            {
                _context.Insurances.Remove(insurance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceExists(int id)
        {
          return (_context.Insurances?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private string GetUserId()
        {
            return _userManager.GetUserId(User)!;
        }
        private bool CheckTimestamp(DateTime start, DateTime end, DateTime newStart, DateTime newEnd)
        {
            Range<DateTime> range = new Range<DateTime>(start, end);
            Range<DateTime> compareRange = new Range<DateTime>(newStart, newEnd);
            return range.IsOverlapped(compareRange);
        }

        private bool CheckOverlap(DateTime start, DateTime end, List<Insurance> insurances)
        {
            foreach (Insurance insurance in insurances)
            {
                if(CheckTimestamp(insurance.Start, insurance.End, start, end)) return true;
            }

            return false;
        }

        //Check if user already have a insurance of a specif type
        private bool CheckInsuranceTypeExists(int? productId)
        {;
            var exists = _context.Insurances.Include(x => x.Product).Where(x => x.UserId == GetUserId()).FirstOrDefault(x => x.ProductId == productId);
            if (exists != null ) return true;
            else return false;
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
