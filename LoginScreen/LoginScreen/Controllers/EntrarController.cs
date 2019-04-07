using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginScreen.Models;
using LoginScreen.Models.ViewModels;

namespace LoginScreen.Controllers
{
    public class EntrarController : Controller
    {
        public IActionResult Logar()
        {
            return View();
        }
        public IActionResult Registrar()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
