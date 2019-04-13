using System.Collections.Generic;
using System.Diagnostics;
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
        public IActionResult Logar(Usuario usuario)
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
        public IActionResult Cadastrar(Usuario usuario)
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
        public IActionResult RecuperarSenha(Usuario usuario)
        {
            if (usuario.senha == usuario.confirmarSenha)
            {
                if (_usuarioService.RecuperarSenha(usuario))
                {
                    ViewData["message"] = "senha alterada";
                    return RedirectToAction("Logar");
                }
                ViewData["erroDeUsuario"] = "O usuário digitado não existe";
            }
            else if (usuario.senha != usuario.confirmarSenha)
            {
                ViewData["erroDeSenha"] = "As senhas digitadas são diferentes";
            }
            return View(usuario);

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

        public IActionResult GerenciarConta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GerenciarConta(UsuarioView usuario)
        {
            if(usuario.nome == null|| usuario.senha == null || usuario.novaSenha== null|| usuario.novaSenha != usuario.confirmaNovaSenha)
            {
                ViewData["erroDeCredenciais"] = "informações inválidas";
            }
            else
            {
                string User = _usuarioService.AlterarRegistro(usuario);
                if (User != null)
                {
                    TempData["Logado"] = User;
                    TempData["sucess"] = "Credenciais alteradas";
                    return RedirectToAction(nameof(Logado));
                }
                
            }
            return View(usuario);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
