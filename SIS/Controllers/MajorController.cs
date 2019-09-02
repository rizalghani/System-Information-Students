using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIS.Data;
using SIS.Models;

namespace SIS.Controllers
{
    public class MajorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MajorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Major
        public async Task<IActionResult> Index()
        {
            return View(await _context.Majors.ToListAsync());
        }

        
        // GET: Major/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Major/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MajorName,MajorCode")] Major major)
        {
            if (ModelState.IsValid)
            {
                _context.Add(major);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(major);
        }

        // GET: Major/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var major = await _context.Majors.FindAsync(id);
            if (major == null)
            {
                return NotFound();
            }
            return View(major);
        }

        // POST: Major/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MajorName,MajorCode")] Major major)
        {
            if (id != major.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(major);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MajorExists(major.ID))
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
            return View(major);
        }

        // POST: Major/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var major = await _context.Majors.FindAsync(id);
                _context.Majors.Remove(major);
                await _context.SaveChangesAsync();

                return Json(new { msg = "success" });
            }
            catch (Exception e)
            {
                return Json(new { msg = e.Message });
            }
        }

        private bool MajorExists(int id)
        {
            return _context.Majors.Any(e => e.ID == id);
        }
    }
}
