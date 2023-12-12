using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment5.Data;
using Assignment5.Models;

namespace Assignment5.Controllers
{
    public class NetUsersController : Controller
    {
        private readonly Assignment5Context _context;

        public NetUsersController(Assignment5Context context)
        {
            _context = context;
        }

        // GET: NetUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.NetUser.ToListAsync());
        }

        // GET: NetUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var netUser = await _context.NetUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (netUser == null)
            {
                return NotFound();
            }

            return View(netUser);
        }

        // GET: NetUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NetUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,UserPassword,UserEmail,UserType")] NetUser netUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(netUser);
                await _context.SaveChangesAsync();
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString(IndexModel.SessionKeyType)) && 
                    string.Equals(HttpContext.Session.GetString(IndexModel.SessionKeyType), "Admin")) {
                    return RedirectToAction(nameof(Index));
                } else
                {
                    HttpContext.Session.SetString(IndexModel.SessionKeyUser, netUser.UserName);
                    HttpContext.Session.SetString(IndexModel.SessionKeyType, netUser.UserType);
                    return RedirectToAction("Index", "Home");
                }
                
            }
            return View(netUser);
        }

        // GET: NetUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var netUser = await _context.NetUser.FindAsync(id);
            if (netUser == null)
            {
                return NotFound();
            }
            return View(netUser);
        }

        // POST: NetUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,UserPassword,UserEmail,UserType")] NetUser netUser)
        {
            if (id != netUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(netUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NetUserExists(netUser.Id))
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
            return View(netUser);
        }

        // GET: NetUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var netUser = await _context.NetUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (netUser == null)
            {
                return NotFound();
            }

            return View(netUser);
        }

        // POST: NetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var netUser = await _context.NetUser.FindAsync(id);
            if (netUser != null)
            {
                _context.NetUser.Remove(netUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NetUserExists(int id)
        {
            return _context.NetUser.Any(e => e.Id == id);
        }
    }
}
