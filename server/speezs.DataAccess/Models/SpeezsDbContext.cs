﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace speezs.DataAccess.Models;

public partial class SpeezsDbContext : DbContext
{
    public SpeezsDbContext(DbContextOptions<SpeezsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BillingAddress> BillingAddresses { get; set; }

    public virtual DbSet<Collectionlook> Collectionlooks { get; set; }

    public virtual DbSet<Favoritecollection> Favoritecollections { get; set; }

    public virtual DbSet<Look> Looks { get; set; }

    public virtual DbSet<Lookproduct> Lookproducts { get; set; }

    public virtual DbSet<Makeupproduct> Makeupproducts { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subscriptiontier> Subscriptiontiers { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<Transfer> Transfers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userclaim> Userclaims { get; set; }

    public virtual DbSet<Userpreference> Userpreferences { get; set; }

    public virtual DbSet<Userresetpasswordcode> Userresetpasswordcodes { get; set; }

    public virtual DbSet<Userrole> Userroles { get; set; }

    public virtual DbSet<Usersubscription> Usersubscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<BillingAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("billing_address_pk");

            entity.HasOne(d => d.User).WithMany(p => p.BillingAddresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("billing_address_users_user_id_fk");
        });

        modelBuilder.Entity<Collectionlook>(entity =>
        {
            entity.HasKey(e => e.CollectionLookId).HasName("collectionlooks_pkey");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.Collection).WithMany(p => p.Collectionlooks).HasConstraintName("collectionlooks_collection_id_fkey");

            entity.HasOne(d => d.Look).WithMany(p => p.Collectionlooks).HasConstraintName("collectionlooks_look_id_fkey");
        });

        modelBuilder.Entity<Favoritecollection>(entity =>
        {
            entity.HasKey(e => e.CollectionId).HasName("favoritecollections_pkey");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.IsPrivate).HasDefaultValue(false);

            entity.HasOne(d => d.User).WithMany(p => p.Favoritecollections).HasConstraintName("favoritecollections_user_id_fkey");
        });

        modelBuilder.Entity<Look>(entity =>
        {
            entity.HasKey(e => e.LookId).HasName("looks_pkey");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.IsPublic).HasDefaultValue(true);
            entity.Property(e => e.TotalTransfers).HasDefaultValue(0);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Looks).HasConstraintName("looks_created_by_fkey");
        });

        modelBuilder.Entity<Lookproduct>(entity =>
        {
            entity.HasKey(e => e.LookProductId).HasName("lookproducts_pkey");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.Look).WithMany(p => p.Lookproducts).HasConstraintName("lookproducts_look_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Lookproducts).HasConstraintName("lookproducts_product_id_fkey");
        });

        modelBuilder.Entity<Makeupproduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("makeupproducts_pkey");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.IsVerified).HasDefaultValue(false);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("reviews_pkey");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.HelpfulVotes).HasDefaultValue(0);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.Look).WithMany(p => p.Reviews).HasConstraintName("reviews_look_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews).HasConstraintName("reviews_user_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pk");
        });

        modelBuilder.Entity<Subscriptiontier>(entity =>
        {
            entity.HasKey(e => e.TierId).HasName("subscriptiontiers_pkey");

            entity.Property(e => e.AllowsCommercialUse).HasDefaultValue(false);
            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.PriorityProcessing).HasDefaultValue(false);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transactions_pk");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.BillingAddress).WithMany(p => p.Transactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transactions_billing_address_id_fk");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transactions_users_user_id_fk");
        });

        modelBuilder.Entity<Transfer>(entity =>
        {
            entity.HasKey(e => e.TransferId).HasName("transfers_pkey");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.Look).WithMany(p => p.Transfers).HasConstraintName("transfers_look_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Transfers).HasConstraintName("transfers_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
        });

        modelBuilder.Entity<Userclaim>(entity =>
        {
            entity.HasKey(e => e.ClaimId).HasName("userclaims_pk");

            entity.Property(e => e.ClaimId).ValueGeneratedNever();
            entity.Property(e => e.ClaimType).HasDefaultValueSql("'UNKNOWN'::character varying");
            entity.Property(e => e.ClaimValue).HasDefaultValueSql("'UNKNOWN'::character varying");

            entity.HasOne(d => d.User).WithMany(p => p.Userclaims)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_id");
        });

        modelBuilder.Entity<Userpreference>(entity =>
        {
            entity.HasKey(e => e.PreferenceId).HasName("userpreferences_pkey");

            entity.Property(e => e.AiEnhancementLevel).HasDefaultValue(5);
            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.User).WithOne(p => p.Userpreference).HasConstraintName("userpreferences_user_id_fkey");
        });

        modelBuilder.Entity<Userresetpasswordcode>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("userresetpasswordcodes_pkey");
        });

        modelBuilder.Entity<Userrole>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("userroles_pk");

            entity.Property(e => e.UserId).ValueGeneratedNever();

            entity.HasOne(d => d.Role).WithMany(p => p.Userroles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userroles_roles_id_fk");

            entity.HasOne(d => d.User).WithOne(p => p.Userrole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userroles_users_user_id_fk");
        });

        modelBuilder.Entity<Usersubscription>(entity =>
        {
            entity.HasKey(e => e.UserSubscriptionId).HasName("usersubscriptions_pkey");

            entity.Property(e => e.AutoRenew).HasDefaultValue(true);
            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.Tier).WithMany(p => p.Usersubscriptions).HasConstraintName("usersubscriptions_tier_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Usersubscriptions).HasConstraintName("usersubscriptions_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}