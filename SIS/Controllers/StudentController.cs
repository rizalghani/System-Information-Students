using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIS.Data;
using SIS.Models;

namespace SIS.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            var students = _context.Students.Include(m => m.Major);
            return View(await students.ToListAsync());
        }


        // GET: Student/Create
        public IActionResult Create()
        {
            ViewBag.Majors = new SelectList(_context.Majors, "ID", "MajorName");
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NIS,Name,BoD,Gender,Religion,MajorID,Address")] Student student)
        {
            if (ModelState.IsValid)
            {
                string dtNow = $"{DateTime.Now.Year}{DateTime.Now.Month.ToString("00")}{DateTime.Now.Day.ToString("00")}{DateTime.Now.Second.ToString("00")}";
                student.NIS = "ST" + dtNow;

                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Majors = new SelectList(_context.Majors, "ID", "MajorName", student.MajorID);
            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewBag.Majors = new SelectList(_context.Majors, "ID", "MajorName", student.MajorID);
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NIS,Name,BoD,Gender,Religion,MajorID,Address")] Student student)
        {
            if (id != student.NIS)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.NIS))
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
            ViewBag.Majors = new SelectList(_context.Majors, "ID", "MajorName", student.MajorID);
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

                return Json(new { msg = "success" });
            }
            catch (Exception e)
            {
                return Json(new { msg = e.Message });
            }
        }

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.NIS == id);
        }
    }
}
