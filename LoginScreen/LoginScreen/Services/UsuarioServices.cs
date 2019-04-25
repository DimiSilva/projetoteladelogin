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

        public int PegarIdPorEmail(string email)
        {
            Usuario user = _context.Usuario.Where(Usuario => Usuario.email == email).SingleOrDefault();
            return user != null ? user.id : 0;
        }
        public Usuario PegarUsuario(int id)
        {
            return _context.Usuario.ToList().Find(user => user.id == id);
        }
        public Usuario PegarUsuario(string nomeDeUsuario)
        {
            return _context.Usuario.ToList().Find(user => user.nomeDeUsuario == nomeDeUsuario);
        }

        public List<Usuario> PegarUsuarios()
        {
            return _context.Usuario.ToList();
        }

        public int Cadastrar(CadastroViewModel obj)
        {
            foreach(Usuario user in _context.Usuario)
            {
                if (user.nomeDeUsuario == obj.nomeDeUsuario)
                {
                    return 0;
                }
            }
            _context.Add(new Usuario(obj.nomeDeUsuario,obj.nome,obj.email,obj.senha, false));
            _context.SaveChanges();
            return _context.Usuario.ToList().Find(user => user.nomeDeUsuario == obj.nomeDeUsuario).id;
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
        public bool UsuarioConfirmado(string nomeDeUsuario)
        {
            if(_context.Usuario.ToList().Find(user => user.nomeDeUsuario == nomeDeUsuario).confirmado == true)
            {
                return true;
            }
            return false;
        }

        public void ConfirmarConta(int id)
        {
            _context.Usuario.ToList().Find(user => user.id == id).confirmado = true;
            _context.SaveChanges();
        }

        public void AlterarSenha(AlterarSenhaViewModel obj, int id)
        {
            PegarUsuario(id).senha = obj.novaSenha;
            _context.SaveChanges();
        }

        public bool ApagarUsuario(int id)
        {
            Usuario usuario = PegarUsuario(id);
            if(usuario!= null)
            {
                _context.Remove(usuario);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public void EditarUsuario(EditarUsuarioViewModel obj, int id)
        {
            Usuario usuario = PegarUsuario(id);
            if(usuario != null)
            {
                usuario.nome = obj.nome;
                usuario.senha = obj.novaSenha;
                _context.SaveChanges();
            }
        }
    }
}
