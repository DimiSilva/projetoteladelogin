using Microsoft.AspNetCore.Mvc;

namespace LoginScreen.Controllers
{
    public class LogadoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}