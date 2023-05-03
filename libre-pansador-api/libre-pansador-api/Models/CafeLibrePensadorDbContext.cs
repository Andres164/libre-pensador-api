using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace libre_pansador_api.Models;

public partial class CafeLibrePensadorDbContext : DbContext
{
    public CafeLibrePensadorDbContext()
    {
    }

    public CafeLibrePensadorDbContext(DbContextOptions<CafeLibrePensadorDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost; Port=5432; Database=CafeLibrePensador; User Id=postgres; Password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("Cards_pkey");

            entity.Property(e => e.CardId)
                .HasMaxLength(7)
                .IsFixedLength()
                .HasColumnName("card_id");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(100)
                .HasColumnName("customer_email");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.LoyverseCustomerId)
                .HasMaxLength(150)
                .HasColumnName("loyverse_customer_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
