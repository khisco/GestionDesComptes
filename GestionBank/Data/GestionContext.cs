using GestionBank.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionBank.Data
{
    public class GestionContext:DbContext
    {
        public GestionContext(DbContextOptions<GestionContext> options):base(options)
        {
            


        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Compte> Comptes { get; set; }
    }
}
