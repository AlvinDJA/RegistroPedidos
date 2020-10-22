using Microsoft.EntityFrameworkCore;
using RegistroPedidos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistroPedidos.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Ordenes> Ordenes { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Suplidores> Suplidores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(@"Data Source=DATA\BDPedidos.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Suplidores>().HasData(
                new Suplidores { SuplidorId = 1, Nombres = "Juan Jose" }
                );

            modelBuilder.Entity<Suplidores>().HasData(
                new Suplidores { SuplidorId = 2, Nombres = "Jose Carlos" }
                );
            modelBuilder.Entity<Suplidores>().HasData(
                new Suplidores { SuplidorId = 3, Nombres = "Carlos Miguel" }
                );

            modelBuilder.Entity<Productos>().HasData(
                new Productos { ProductoId = 1, Descripcion = "Pasta Dental", Costo = 100, Inventario = 5 }
                );

            modelBuilder.Entity<Productos>().HasData(
                new Productos { ProductoId = 2, Descripcion = "Enjuague Bucal", Costo = 150, Inventario = 3 }
                );

            modelBuilder.Entity<Productos>().HasData(
                new Productos { ProductoId = 3, Descripcion = "Cepillo Dental", Costo = 45, Inventario = 10 }
                );
        }
    }
}
