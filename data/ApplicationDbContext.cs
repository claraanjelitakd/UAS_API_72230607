using System;
using Microsoft.EntityFrameworkCore;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<SaleItems> SaleItems { get; set; }
        public DbSet<AspUser> AspUsers { get; set; }= null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.CategoryID);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(c => c.CustomerId);
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(p => p.ProductId);
                entity.Property(p => p.Price).HasPrecision(18, 2);
            });

            modelBuilder.Entity<Sales>(entity =>
            {
                entity.HasKey(s => s.SaleId);
                entity.Property(s => s.TotalAmount).HasPrecision(18, 2);
            });

            modelBuilder.Entity<SaleItems>(entity =>
            {
                entity.HasKey(si => si.SaleItemId);
                entity.Property(si => si.Price).HasPrecision(18, 2);
            });
        }
    }
}
