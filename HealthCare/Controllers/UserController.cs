﻿using HealthCare.Areas.Identity.Data;
using HealthCare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            return _context.Users != null ?
                        View(await _context.Users.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Users'  is null.");
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,UserName,Id")] ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FirstName,LastName,Email,UserName,Id")] ApplicationUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            var userExists = _context.Users.FirstOrDefault(x => x.Id == id);

            if(userExists == null)
            {
                return NotFound();
            }

            //Update data
            userExists.FirstName= user.FirstName;
            userExists.LastName= user.LastName;
            userExists.Email= user.Email;
            userExists.UserName = user.UserName;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Users.Update(userExists);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(userExists);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
            }

            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                if (DeleteAllInsurances(id) && DeleteAllAppointments(id)) 
                {
                    _context.Users.Remove(user);
                }               
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool DeleteAllInsurances(string userId)
        {
            var insurances = _context.Insurances.Where(x => x.UserId == userId).ToList();

            foreach (var item in insurances)
            {
                try
                {
                    _context.Insurances.Remove(item);                 
                }
                catch (ArgumentException e)
                {
                    return false;
                }             
            }
            return true;
        }

        private bool DeleteAllAppointments(string userId)
        {
            var appointments = _context.Appointments.Where(x => x.UserId == userId).ToList();

            foreach (var item in appointments)
            {
                try
                {
                    _context.Appointments.Remove(item);
                }
                catch (ArgumentException e)
                {
                    return false;
                }             
            }
            return true;
        }
    }
}

