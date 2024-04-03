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
            // Read a file
            teachers = LoadTeacherFromFile("data.json");
            return View(teachers);
        }
    }
}

