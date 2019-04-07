using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LoginScreen.Models
{
    public class LoginScreenContext : DbContext
    {
        public LoginScreenContext (DbContextOptions<LoginScreenContext> options)
            : base(options)
        {
        }

        public DbSet<LoginScreen.Models.Usuario> Usuario { get; set; }
    }
}
