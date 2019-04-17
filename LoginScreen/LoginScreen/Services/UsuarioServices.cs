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

        public bool Cadastrar(CadastroViewModel obj)
        {
            foreach(Usuario user in _context.Usuario)
            {
                if (user.nomeDeUsuario == obj.nomeDeUsuario)
                {
                    return false;
                }
            }
            _context.Add(new Usuario(obj.nomeDeUsuario,obj.nome,obj.email,obj.senha));
            _context.SaveChanges();
            return true;
        }
        public bool Logar(LoginViewModel obj)
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
    }
}
