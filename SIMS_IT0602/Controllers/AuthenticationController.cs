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
    public class AuthenticationController : Controller
    {
        [HttpPost]
        public IActionResult Login(User user)
        {
            //read file users.json
            List<User>? users = LoadUsersFromFile("users.json");
            var result = users.
                        FirstOrDefault(u => u.UserName == user.UserName
                                && u.Pass == user.Pass);
            if (result != null)
            {
                HttpContext.Session.SetString("UserName", result.UserName);
                HttpContext.Session.SetString("Role", result.Role);
                return RedirectToAction("Index", "Teacher");
            }
            else
            {
                ViewBag.error = "Invalid user!";
                return View("Login");
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public List<User>? LoadUsersFromFile(string fileName)
        {
            string readText = System.IO.File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<User>>(readText);
        }
    }
}

