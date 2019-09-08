using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SIS.Data;
using SIS.Models;

namespace SIS.Controllers
{
    [Authorize] //Jika authorize di aktifkan maka untuk memasuki halaman PPG harus login dulu
    /*[Table("PPG")]*/ //Jika nama table berbeda dengan class model bisa di setting disini
    public class PPGController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;

        public PPGController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
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


        [HttpPost]
        public async Task<IActionResult> Import(IFormFile files)
        {
            string folderName = "upload";
            string path = _env.WebRootPath;
            string pathNew = Path.Combine(path, folderName);
            string errorMessage = "";

            if (files != null)
            {
                // Crate New Directory if not exist
                if (!Directory.Exists(pathNew))
                {
                    Directory.CreateDirectory(pathNew);
                }


                var upload = Path.Combine(pathNew, files.FileName);

                // Upload file to folder
                if (files != null)
                {
                    if (files.Length > 0)
                    {
                        using (var stream = new FileStream(Path.Combine(upload), FileMode.Create))
                        {
                            await files.CopyToAsync(stream);
                        }
                    }
                }

                // Import Excel to DB
                FileInfo fileInfo = new FileInfo(upload);
                using (ExcelPackage excel = new ExcelPackage(fileInfo))
                {
                    try
                    {
                        ExcelWorksheet worksheet = excel.Workbook.Worksheets["Data"];
                        int totalRows = worksheet.Dimension.Rows;
                        List<PPG> studentList = new List<PPG>();
                        int no = 1;

                        for (int i = 2; i <= totalRows; i++)
                        {
                            try
                            {
                                string _No_Ujian = worksheet?.Cells[i, 1]?.Value?.ToString();
                                string _Nopes_PPG = worksheet?.Cells[i, 2]?.Value?.ToString();
                                string _Nama = worksheet?.Cells[i, 3]?.Value?.ToString();
                                string _Lokasi_PPG = worksheet?.Cells[i, 4]?.Value?.ToString();
                                string _Mapel = worksheet?.Cells[i, 5]?.Value?.ToString();
                                string _Stat_Gabungan = worksheet?.Cells[i, 6]?.Value?.ToString();

                                bool nopes_ppg = _context.PPG.Where(x => x.No_PPG.Equals(_Nopes_PPG)).Any();

                                if (!nopes_ppg)
                                {
                                    studentList.Add(new PPG
                                    {
                                        No_Ujian = _No_Ujian,
                                        No_PPG = _Nopes_PPG,
                                        Nama = _Nama,
                                        Lokasi = _Lokasi_PPG,
                                        Mapel = _Mapel,
                                        Status = _Stat_Gabungan
                                    });
                                }
                                else
                                {
                                    errorMessage = "Input File Error !";
                                }

                            }
                            catch (Exception ex)
                            {
                                errorMessage = "There is an error in inputting Excel data, please check your Excel file again, " + ex.Message;
                            }

                            no++;

                        }

                        await _context.PPG.AddRangeAsync(studentList);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception x)
                    {
                        errorMessage = "File Excel Error !, " + x.Message;
                    }
                }

                // Remove Excel file after upload process
                //string pathOld = Path.Combine(pathNew, files.FileName);
                //if (System.IO.File.Exists(pathOld))
                //{
                //    System.IO.File.Delete(pathOld);
                //}
                ViewBag.errorMsg = errorMessage;
                
            }

            return RedirectToAction("Index");

        }
    }
}