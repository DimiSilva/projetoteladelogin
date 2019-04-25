using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using LoginScreen.Models;
using LoginScreen.Models.ViewModels;
using LoginScreen.Services;
using System;
using MailKit.Net.Smtp;
using MimeKit;

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
            TempData["Layout"] = "~/Views/Shared/_Entrar.cshtml";
            TempData["Logado"] = null;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logar(LoginViewModel usuario)
        {
            TempData["Layout"] = "~/Views/Shared/_Entrar.cshtml";
            if (_usuarioService.Logar(usuario))
            {
                if (_usuarioService.UsuarioConfirmado(usuario.nomeDeUsuario))
                {
                    TempData["Logado"] = _usuarioService.PegarUsuario(usuario.nomeDeUsuario).id;
                    return RedirectToAction("Principal", "Logado");
                }
                else
                {
                    TempData["IdParaConfirmar"] = _usuarioService.PegarUsuario(usuario.nomeDeUsuario).id;
                    return RedirectToAction(nameof(ConfirmarConta));
                }
                
            }
            ViewData["ErroDeCredencias"] = "Credenciais inválidas";
            return View(usuario);
        }
        public IActionResult Cadastrar()
        {
            TempData["Layout"] = "~/Views/Shared/_Entrar.cshtml";
            TempData["Logado"] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(CadastroViewModel usuario)
        {
            TempData["Layout"] = "~/Views/Shared/_Entrar.cshtml";
            if (ModelState.IsValid)
            {
                if (_usuarioService.PegarIdPorEmail(usuario.email) == 0)
                {
                    int id = _usuarioService.Cadastrar(usuario);
                    if (id != 0)
                    {
                        TempData["IdParaConfirmar"] = id;
                        return RedirectToAction(nameof(ConfirmarConta));
                    }
                    else
                    {
                        ViewData["UsuarioErro"] = "O Nome de usuário em questão já está cadastrado, tente outro";
                        return View(usuario);
                    }
                }
                else
                {
                    ViewData["EmailErro"] = "O email em questão já está cadastrado";
                }
            }
            return View(usuario);
        }
        public IActionResult ConfirmarConta()
        {
            TempData["Layout"] = "~/Views/Shared/_Entrar.cshtml";
            int id = int.Parse(TempData["IdParaConfirmar"].ToString());
            if(id == 0)
            {
                return BadRequest();
            }
            Random rand = new Random();
            int codigo = rand.Next(100000, 999999);
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("loginscreenconfirmation@gmail.com"));
            message.To.Add(new MailboxAddress(_usuarioService.PegarUsuario(id).email.ToString()));
            message.Subject = "Confirmação da conta";
            message.Body = new TextPart("plain")
            {
                Text = "O código para confirmar a sua conta é " + codigo
            };

            using(var cliente = new SmtpClient())
            {
                cliente.Connect("smtp.gmail.com", 587, false);
                cliente.Authenticate("loginscreenconfirmation@gmail.com", "Adgswasfffcvb_15483286421321058");
                cliente.Send(message);
                cliente.Disconnect(true);
            }
            TempData["IdParaConfirmar"] = id;
            TempData["codigo"] = codigo;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarConta(ConfirmacaoPorEmailViewModel user)
        {
            TempData["Layout"] = "~/Views/Shared/_Entrar.cshtml";
            int id = int.Parse(TempData["IdParaConfirmar"].ToString());
            int codigo = int.Parse(TempData["codigo"].ToString());
            if (user.codigo == codigo)
            {
                _usuarioService.ConfirmarConta(id);
                TempData["Logado"] = id;
                return RedirectToAction("Principal","Logado");
            }
            else
            {
                TempData["id"] = id;
                TempData["codigo"] = codigo;
                ViewBag.codigoInvalido = "O codigo digitado é inválido";
                return View();
            }
        }

        public IActionResult RecuperarSenha()
        {
            TempData["Layout"] = "~/Views/Shared/_Entrar.cshtml";
            TempData["Logado"] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RecuperarSenha(RecuperarSenhaViewModel usuario)
        {
            TempData["Layout"] = "~/Views/Shared/_Entrar.cshtml";
            if (ModelState.IsValid)
            {
                int id = _usuarioService.PegarIdPorEmail(usuario.email);
                if (id != 0)
                {
                    Random rand = new Random();
                    int codigo = rand.Next(100000, 999999);
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("loginscreenconfirmation@gmail.com"));
                    message.To.Add(new MailboxAddress(_usuarioService.PegarUsuario(id).email.ToString()));
                    message.Subject = "Recuperação de conta";
                    message.Body = new TextPart("plain")
                    {
                        Text = "O código para recuperar sua conta é " + codigo
                    };

                    using (var cliente = new SmtpClient())
                    {
                        cliente.Connect("smtp.gmail.com", 587, false);
                        cliente.Authenticate("loginscreenconfirmation@gmail.com", "Adgswasfffcvb_15483286421321058");
                        cliente.Send(message);
                        cliente.Disconnect(true);
                    }
                    TempData["IdParaRecuperar"] = id;
                    TempData["codigoRecuperar"] = codigo;
                        return RedirectToAction(nameof(ConfirmarRecuperarSenha));
                }
            }
            return View(usuario);
        }
        public IActionResult ConfirmarRecuperarSenha()
        {
            TempData["Layout"] = "~/Views/Shared/_Entrar.cshtml";
            TempData["Logado"] = null;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarRecuperarSenha(ConfirmacaoPorEmailViewModel user)
        {
            TempData["Layout"] = "~/Views/Shared/_Entrar.cshtml";
            int id = int.Parse(TempData["IdParaRecuperar"].ToString());
            if (id == 0)
            {
                return BadRequest();
            }
            int codigo = int.Parse(TempData["codigoRecuperar"].ToString());
            if (ModelState.IsValid)
            {
                if (user.codigo == codigo)
                {
                    TempData["IdParaRecuperar"] = id;
                    return RedirectToAction(nameof(AlterarSenha));
                }
                ViewBag.codigoInvalido = "codigo digitado inválido";
            }
            TempData["IdParaRecuperar"] = id;
            TempData["codigoRecuperar"] = codigo;
            return View();
        }

        public IActionResult AlterarSenha()
        {
            TempData["Layout"] = "~/Views/Shared/_Entrar.cshtml";
            TempData["Logado"] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AlterarSenha(AlterarSenhaViewModel obj)
        {
            TempData["Layout"] = "~/Views/Shared/_Entrar.cshtml";
            int id = int.Parse(TempData["IdParaRecuperar"].ToString());
            if (id == 0)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                _usuarioService.AlterarSenha(obj, id);
                return RedirectToAction(nameof(Logar));
            }
            return View();
        }
    }
}
