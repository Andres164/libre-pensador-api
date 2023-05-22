using System;
using System.Collections.Generic;
using System.Text;
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

    public virtual DbSet<LocalCustomer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("Cards_pkey");

            entity.Property(e => e.CardId)
                .HasMaxLength(7)
                .IsFixedLength()
                .IsRequired()
                .HasColumnName("card_id");
            entity.Property(e => e.CustomerEmail)
                .HasConversion(new Converters.EncryptionConverter())
                .HasColumnType("bytea")
                .HasColumnName("customer_email");
        });

        modelBuilder.Entity<LocalCustomer>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasConversion(new Converters.EncryptionConverter()!)
                .HasColumnType("bytea")
                .HasColumnName("email");
            entity.Property(e => e.DateOfBirth)
                .IsRequired()
                .HasConversion(new Converters.EncryptionConverter()!)
                .HasColumnType("bytea")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.LoyverseCustomerId)
                .HasMaxLength(150)
                .IsRequired()
                .HasColumnName("loyverse_customer_id");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("employees_pkey");

            entity.ToTable("employees");

            entity.Property(e => e.UserName)
                .IsRequired()
                .HasConversion(new Converters.EncryptionConverter()!)
                .HasColumnType("bytea")
                .HasColumnName("user_name");

            entity.Property(e => e.Password)
                .IsRequired()
                .HasConversion(new Converters.EncryptionConverter()!)
                .HasColumnType("bytea")
                .HasColumnName("password");

            entity.Property(e => e.IsAdmin)
                .IsRequired()
                .HasColumnType("boolean")
                .HasColumnName("is_admin");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    
}
