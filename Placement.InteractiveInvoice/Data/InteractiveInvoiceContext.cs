using Microsoft.EntityFrameworkCore;
using Placement.InteractiveInvoice.Models;

namespace Placement.InteractiveInvoice.Data
{
    public class InteractiveInvoiceContext : DbContext
    {
        public InteractiveInvoiceContext(DbContextOptions<InteractiveInvoiceContext> options) : base(options)
        {

        }

        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=interactiveinvoice.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceLineItem>()
                .HasKey(bc => new { bc.InvoiceID, bc.LineItemID });
            modelBuilder.Entity<InvoiceLineItem>()
                .HasOne(bc => bc.Invoice)
                .WithMany(b => b.InvoiceLineItems)
                .HasForeignKey(bc => bc.LineItemID);
            modelBuilder.Entity<InvoiceLineItem>()
                .HasOne(bc => bc.LineItem)
                .WithMany(c => c.InvoiceLineItems)
                .HasForeignKey(bc => bc.InvoiceID);
        }
    }
}
