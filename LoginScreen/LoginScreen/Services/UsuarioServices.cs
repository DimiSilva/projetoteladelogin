using LoginScreen.Models;
using System.Collections.Generic;
using System.Linq;
using LoginScreen.Models.ViewModels;

namespace LoginScreen.Services
{
    public class UsuarioServices
    {
        private readonly LoginScreenContext _context;

        public UsuarioServices(LoginScreenContext context)
        {
            _context = context;
        }

        public List<Usuario> PegarUsuarios()
        {
            return _context.Usuario.ToList();
        }

        public bool Cadastrar(Usuario obj)
        {
            foreach(Usuario user in _context.Usuario)
            {
                if (user.nomeDeUsuario == obj.nomeDeUsuario)
                {
                    return false;
                }
            }
            _context.Add(obj);
            _context.SaveChanges();
            return true;
        }
        public bool Logar(Usuario obj)
        {
            foreach (Usuario user in _context.Usuario.ToList())
            {
                if(user.nomeDeUsuario == obj.nomeDeUsuario && user.senha == obj.senha)
                {
                    return true;
                }
            }
            return false;
        }
        public bool RecuperarSenha(Usuario obj)
        {
            foreach(Usuario User in _context.Usuario.ToList())
            {
                if(User.nomeDeUsuario == obj.nomeDeUsuario)
                {
                    User.senha = obj.senha;
                    User.confirmarSenha = obj.confirmarSenha;
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public string AlterarRegistro(UsuarioView obj) {
            foreach(Usuario item in _context.Usuario.ToList())
            {
                if(item.nome == obj.nome && item.senha == obj.senha)
                {
                    item.nome = obj.nome;
                    item.senha = obj.novaSenha;
                    item.confirmarSenha = obj.novaSenha;
                    _context.SaveChanges();
                    return item.nomeDeUsuario;
                }
            }
            return null;
        }
    }
}
