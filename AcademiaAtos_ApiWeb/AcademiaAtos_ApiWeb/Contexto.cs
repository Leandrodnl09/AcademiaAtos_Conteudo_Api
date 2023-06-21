using AcademiaAtos_ApiWeb.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace AcademiaAtos_ApiWeb
{
    public class Contexto : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Email> Emails { get; set; }

        public Contexto() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source = localhost; Initial Catalog = AcademiaAtos_CodeFirst; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");

            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Email>()
                .HasOne(e => e.pessoa)
                .WithMany(e => e.emails)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
