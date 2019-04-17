using System.Linq;
using LoginScreen.Models;

namespace LoginScreen.Data
{
    public class SeedService
    {
        private LoginScreenContext _context;

        public SeedService(LoginScreenContext context)
        {
            _context = context;
        }

        public void seed()
        {
            if (_context.Usuario.Any())
            {
                return;
            }
            Usuario admin = new Usuario(1, "Admin","Admin", "Admin@gmail.com", "adm123456");
            _context.Usuario.Add(admin);
            _context.SaveChanges();
        }

    }
}
