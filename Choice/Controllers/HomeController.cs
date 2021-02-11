using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Choice.Models;
using Choice.Data;
using Microsoft.AspNetCore.Authorization;

namespace Choice.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            
            return View(_db.Users);
        }
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult GetDisciplines(string id)
        {
            var student = _db.Users.Where(x => x.Id == id).First();
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
            var student = _db.Users.Find(id);
            student.Disciplines = new();
            for (int i=0; i < _db.Disciplines.Count(); i++)
            {
                if(disciplinesSelect.ElementAt(i))
                {
                    student.Disciplines.Add(_db.Disciplines.Find(i));
                }
            }            
            _db.SaveChanges();
            return View("Index", _db.Users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
