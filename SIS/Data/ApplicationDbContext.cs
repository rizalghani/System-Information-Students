using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SIS.Models;

namespace SIS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

<<<<<<< HEAD
        public virtual DbSet<PPG> PPG { get; set; }
=======
        //<PPG> adalah nama model PPG.cs, sedangkan PPG adalah nama tabel di DB
        public DbSet<PPG> PPG { get; set; }
>>>>>>> 294fb5a3e40fc9bab6939a88b4e64f7d4d287a8a
    }
}
