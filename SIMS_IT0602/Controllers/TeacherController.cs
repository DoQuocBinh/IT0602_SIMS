using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIMS_IT0602.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIMS_IT0602.Controllers
{
    public class TeacherController : Controller
    {
        static List<Teacher> teachers = new List<Teacher>();

        public IActionResult Delete(int Id)
        {
            var teachers = LoadTeacherFromFile("data.json");

            //find teacher in an array
            var searchTeacher = teachers.FirstOrDefault(t => t.Id == Id);
            teachers.Remove(searchTeacher);

            //save to file
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(teachers, options);
            //Save file
            using (StreamWriter writer = new StreamWriter("data.json"))
            {
                writer.Write(jsonString);
            }
            return RedirectToAction("Index");

        }

        [HttpPost] //submit new Teacher
        public IActionResult NewTeacher(Teacher teacher)
        {
            teachers.Add(teacher);
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(teachers, options);
            //Save file
            using (StreamWriter writer = new StreamWriter("data.json"))
            {
                writer.Write(jsonString);
            }

            return Content(jsonString);
        }

        [HttpGet] //click hyperlink
        public IActionResult NewTeacher()
        {
            return View();
        }
        public List<Teacher>? LoadTeacherFromFile(string fileName)
        {
            string readText = System.IO.File.ReadAllText("data.json");
            return JsonSerializer.Deserialize<List<Teacher>>(readText);
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            // Read a file
            teachers = LoadTeacherFromFile("data.json");
            return View(teachers);
        }
    }
}

