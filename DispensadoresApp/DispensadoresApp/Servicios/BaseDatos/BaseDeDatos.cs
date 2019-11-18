using DispensadoresApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DispensadoresApp.Servicios.BaseDatos
{
    public class BaseDeDatos : DbContext
    {
        public DbSet<UsuarioModelo> Usuarios { get; set; }

        public BaseDeDatos()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = DependencyService.Get<IBaseDatos>().GetDatabasePath();
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UsuarioModelo>().HasKey(u => u.Id_Usuario);

        }
    }
}
