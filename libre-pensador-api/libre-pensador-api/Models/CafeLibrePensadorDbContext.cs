﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace libre_pensador_api.Models;

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

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<LogEntry> LogEntries { get; set; }

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
            entity.Property(e => e.EncryptedCustomerEmail)
                .HasColumnType("bytea")
                .HasColumnName("customer_email");
        });

        modelBuilder.Entity<LocalCustomer>(entity =>
        {
            entity.HasKey(e => e.EncryptedEmail).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.Property(e => e.EncryptedEmail)
                .IsRequired()
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

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("user_name");

            entity.Property(e => e.Password)
                .IsRequired()
                .HasConversion(new Converters.HashConverter())
                .HasColumnType("bytea")
                .HasColumnName("password");

            entity.Property(e => e.IsAdmin)
                .IsRequired()
                .HasColumnType("boolean")
                .HasColumnName("is_admin");
        });

        modelBuilder.Entity<LogEntry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("log_entries_pkey");

            entity.ToTable("log_entries");

            entity.Property(e => e.Id)
                .UseIdentityColumn()
                .HasColumnName("id");

            entity.Property(e => e.LogLevel)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("log_level");

            entity.Property(e => e.Message)
                .IsRequired()
                .HasColumnName("message");

            entity.Property(e => e.Exception)
                .HasColumnName("exception");

            entity.Property(e => e.StackTrace)
                .HasColumnName("stack_trace");

            entity.Property(e => e.OccurredOn)
                .IsRequired()
                .HasColumnType("timestamp without time zone")
                .HasColumnName("occurred_on");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    
}
