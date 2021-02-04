using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Students);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult GetDisciplines()
        {
            ViewBag.Disciplines = new SelectList(_db.Disciplines, "Id", "Title");
            return View();
        }
        [HttpPost]
        public IActionResult GetDisciplines(int id, IEnumerable<Discipline> disciplines)
        {
            _db.Students.Find(id).Disciplines = disciplines.ToList();
            _db.SaveChanges();
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
