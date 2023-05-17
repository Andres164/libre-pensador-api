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
                .HasColumnType("bytea")
                .HasColumnName("customer_email");
        });

        modelBuilder.Entity<LocalCustomer>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasColumnType("bytea")
                .HasColumnName("email");
            entity.Property(e => e.EncryptedDateOfBirth)
                .IsRequired()
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
                .HasMaxLength(30)
                .IsRequired()
                .HasColumnName("user_name");

            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsRequired()
                .HasColumnName("password");

            entity.Property(e => e.IsAdmin)
                .IsRequired()
                .HasColumnName("is_admin");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override int SaveChanges()
    {
        EncryptSensitiveData();
        var result = base.SaveChanges();
        DecryptSensitiveData();
        return result;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        EncryptSensitiveData();
        var result = await base.SaveChangesAsync(cancellationToken);
        DecryptSensitiveData();
        return result;
    }

    private void EncryptSensitiveData()
    {
        foreach (var entry in ChangeTracker.Entries<LocalCustomer>())
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                entry.Entity.Email = EncryptionUtility.Encrypt(entry.Entity.Email);
                string dateOfBirth = entry.Entity.EncryptedDateOfBirth ?? "";
                entry.Entity.EncryptedDateOfBirth = EncryptionUtility.Encrypt(dateOfBirth);
            }
        }

        foreach (var entry in ChangeTracker.Entries<Card>())
        {
            var customerEmail = entry.Entity.CustomerEmail;
            if ((entry.State == EntityState.Added || entry.State == EntityState.Modified) && customerEmail != null)
                entry.Entity.CustomerEmail = EncryptionUtility.Encrypt(customerEmail);
        }
    }

    private void DecryptSensitiveData()
    {
        foreach (var entry in ChangeTracker.Entries<LocalCustomer>())
        {
            if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
            {
                entry.Entity.Email = EncryptionUtility.Decrypt(entry.Entity.Email);
                string dateOfBirth = entry.Entity.EncryptedDateOfBirth ?? "";
                entry.Entity.DateOfBirth = DateOnly.Parse(EncryptionUtility.Decrypt(dateOfBirth));
            }
        }

        foreach (var entry in ChangeTracker.Entries<Card>())
        {
            var customerEmail = entry.Entity.CustomerEmail;
            if ((entry.State == EntityState.Added || entry.State == EntityState.Modified) && customerEmail != null)
                entry.Entity.CustomerEmail = EncryptionUtility.Decrypt(customerEmail);
        }
    }
}
