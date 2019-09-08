using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIS.Data;
using SIS.Models;

namespace SIS.Controllers
{
    [Authorize] //Jika authorize di aktifkan maka untuk memasuki halaman PPG harus login dulu
    /*[Table("PPG")]*/ //Jika nama table berbeda dengan class model bisa di setting disini
    public class PPGController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PPGController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PPG
        public async Task<IActionResult> Index()
        {
            return View(await _context.PPG.ToListAsync());
        }


        // GET: PPG/Create
        public IActionResult Create(int id)
        {
            var ppg = _context.PPG.Where(x => x.ID.Equals(id)).FirstOrDefault();
            return View(ppg);
        }

        // POST: PPG/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PPG ppg)
        {
            try
            {
                if (ppg.ID != 0) //EDIT
                {
                    if (ModelState.IsValid)
                    {
                        _context.Update(ppg);
                    }
                }
                else //CREATE
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(ppg);
                    }
                }

                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }


        // POST: PPG/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ppg = await _context.PPG.FindAsync(id);
                _context.PPG.Remove(ppg);
                await _context.SaveChangesAsync();

                return Json(new { msg = "success" });
            }
            catch (Exception e)
            {
                return Json(new { msg = e.Message });
            }
        }
    }
}