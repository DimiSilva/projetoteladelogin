using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginScreen.Data;
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
            Usuario admin = new Usuario(1, "Admin","Admin", "Admin@gmail.com", "adm123456", "adm123456");
            _context.Usuario.Add(admin);
            _context.SaveChanges();
        }

    }
}
