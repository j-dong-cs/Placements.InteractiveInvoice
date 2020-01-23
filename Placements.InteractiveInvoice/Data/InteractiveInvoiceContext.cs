using System;
using Microsoft.EntityFrameworkCore;
using Placements.InteractiveInvoice.Models;

namespace Placements.InteractiveInvoice.Data
{
    public class InteractiveInvoiceContext : DbContext
    {
        public DbSet<Lineitem> Lineitems { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<User> Users { get; set; }

        public InteractiveInvoiceContext(DbContextOptions<InteractiveInvoiceContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1401; Database=invoiceDB; User=SA; Password=Bambiedjj829");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceLineitem>()
                .HasKey(bc => new { bc.InvoiceID, bc.LineitemID });
            modelBuilder.Entity<InvoiceLineitem>()
                .HasOne(bc => bc.Invoice)
                .WithMany(b => b.InvoiceLineitems)
                .HasForeignKey(bc => bc.LineitemID);
            modelBuilder.Entity<InvoiceLineitem>()
                .HasOne(bc => bc.Lineitem)
                .WithMany(c => c.InvoiceLineitems)
                .HasForeignKey(bc => bc.InvoiceID);

            modelBuilder.Entity<Lineitem>()
                .Property(l => l.BookedAmount)
                .HasColumnType("decimal(30,20)");

            modelBuilder.Entity<Lineitem>()
                .Property(l => l.ActualAmount)
                .HasColumnType("decimal(30,20)");

            modelBuilder.Entity<Lineitem>()
                .Property(l => l.Adjustments)
                .HasColumnType("decimal(30,20)");
        }
    }
}
