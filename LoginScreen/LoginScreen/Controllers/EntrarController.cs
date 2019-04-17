using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using LoginScreen.Models;
using LoginScreen.Models.ViewModels;
using LoginScreen.Services;

namespace LoginScreen.Controllers
{
    public class EntrarController : Controller
    {
        private readonly UsuarioServices _usuarioService;

        public EntrarController(UsuarioServices usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public IActionResult Logar()
        {
            TempData["Logado"] = null;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logar(LoginViewModel usuario)
        {
            if (_usuarioService.Logar(usuario))
            {
                TempData["Logado"] = usuario.nomeDeUsuario;
                return RedirectToAction("Logado");
            }
            ViewData["ErroDeCredencias"] = "Credenciais inválidas";
            return View(usuario);
        }
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(CadastroViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                if (usuario.senha == usuario.confirmarSenha)
                {
                    if (_usuarioService.Cadastrar(usuario))
                    {
                        TempData["Sucess"] = "Seja bem vindo " + usuario.nomeDeUsuario;
                        return RedirectToAction(nameof(Logar));
                    }
                    else
                    {
                        ViewData["UsuarioErro"] = "O Nome de usuário em questão já está cadastrado, tente outro";
                        return View(usuario);
                    }
                    
                }
                else
                {
                    ViewData["ConfirmErro"] = "as senhas não batem";
                    return View(usuario);
                }
            }
            return View(usuario);
        }

        public IActionResult RecuperarSenha()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RecuperarSenha(RecuperarSenhaViewModel usuario)
        {
            return View();
        }


        public IActionResult Logado()
        {
            if (TempData["Logado"]!= null)
            {
                List<Usuario> usuarios = _usuarioService.PegarUsuarios();
                return View(usuarios);
            }
            return BadRequest();
        }
    }
}
