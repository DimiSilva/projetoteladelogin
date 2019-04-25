using LoginScreen.Services;
using LoginScreen.Models;
using LoginScreen.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LoginScreen.Controllers
{
    public class LogadoController : Controller
    {
        private readonly UsuarioServices _usuarioService;
        public LogadoController(UsuarioServices usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public IActionResult Principal()
        {
            TempData["Layout"] = "~/Views/Shared/_Logado.cshtml";
            int logado = int.Parse(TempData["logado"].ToString());
            if (logado != 0)
            {
                TempData["Logado"] = logado;
                ViewData["UsuarioLogado"] = _usuarioService.PegarUsuario(logado).nome;
                ViewData["IdLogado"] = _usuarioService.PegarUsuario(logado).id;
                return View(_usuarioService.PegarUsuarios());
            }
            return RedirectToAction("Logar", "Entrar");
        }
        public IActionResult EditarUsuario(int? id)
        {
            TempData["Layout"] = "~/Views/Shared/_Logado.cshtml";
            TempData["idEditar"] = id.Value;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarUsuario(EditarUsuarioViewModel obj)
        {
            TempData["Layout"] = "~/Views/Shared/_Logado.cshtml";
            int logado = int.Parse(TempData["logado"].ToString());
            if (ModelState.IsValid)
            {
                if (logado != 0)
                {
                    TempData["logado"] = logado;
                    _usuarioService.EditarUsuario(obj, int.Parse(TempData["idEditar"].ToString()));
                    return RedirectToAction(nameof(Principal));
                }
                else
                {
                    return BadRequest();
                }

            }
            return View(obj);
        }

        public IActionResult DeletarUsuario(int? id)
        {
            TempData["Layout"] = "~/Views/Shared/_Logado.cshtml";
            int logado = int.Parse(TempData["logado"].ToString());
            if(logado != 0)
            {
                TempData["logado"] = logado;
                if (id == null)
                {
                    return NotFound();
                }
                else if (_usuarioService.ApagarUsuario(id.Value))
                {
                    return RedirectToAction(nameof(Principal));
                }
            }
            else
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Principal));
        }
    }
}