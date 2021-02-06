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
        public IActionResult GetDisciplines(int id)
        {
            var student = _db.Students.Where(x => x.Id == id).First();
            var disciplinesSelect = new List<bool>();
            foreach (var disc in _db.Disciplines)
            {
                if (student.Disciplines != null && student.Disciplines.Contains(disc))
                    disciplinesSelect.Add(true);
                else
                    disciplinesSelect.Add(false); 
            }
            ViewData["DisciplinesSelect"] = disciplinesSelect;
            ViewData["Disciplines"] = _db.Disciplines.ToList();
            return View(student) ;
        }
        [HttpPost]
        public IActionResult GetDisciplines(int id, IEnumerable<bool> disciplinesSelect)
        {            
            var student = _db.Students.Find(id);
            student.Disciplines = new();
            for (int i=0; i < _db.Disciplines.Count(); i++)
            {
                if(disciplinesSelect.ElementAt(i))
                {
                    student.Disciplines.Add(_db.Disciplines.Find(i));
                }
            }            
            _db.SaveChanges();           
            return View("Index", _db.Students);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
