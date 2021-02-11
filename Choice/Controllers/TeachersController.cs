using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Choice.Models;
using Choice.Data;
using Microsoft.AspNetCore.Authorization;

namespace Choice.Controllers
{
    [Authorize]
    public class TeachersController : Controller
    {
        private ApplicationDbContext db;
        public TeachersController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View(db.Teachers);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(string name)
        {
            var teacher = new Teacher { Name = name};
            db.Teachers.Add(teacher);
            db.SaveChanges();
            return View();
        }
        public IActionResult Edit(int id)
        {
            return View(db.Teachers.Find(id));
        }
        [HttpPost]
        public IActionResult Edit(Teacher teacher)
        {
            db.Teachers.Find(teacher.Id).Name = teacher.Name;
            db.SaveChanges();
            return View();
        }
        public IActionResult Delete(int id)
        {
            return View(db.Teachers.Find(id));
        }
        [HttpPost]
        public IActionResult Delete(Teacher teacher)
        {
            db.Remove(teacher);
            db.SaveChanges();
            return View();
        }
    }
}
