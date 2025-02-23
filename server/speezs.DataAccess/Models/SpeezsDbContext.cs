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

    public virtual DbSet<Collectionlook> Collectionlooks { get; set; }

    public virtual DbSet<Favoritecollection> Favoritecollections { get; set; }

    public virtual DbSet<Look> Looks { get; set; }

    public virtual DbSet<Lookproduct> Lookproducts { get; set; }

    public virtual DbSet<Makeupproduct> Makeupproducts { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Subscriptiontier> Subscriptiontiers { get; set; }

    public virtual DbSet<Transfer> Transfers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userclaim> Userclaims { get; set; }

    public virtual DbSet<Userpreference> Userpreferences { get; set; }

    public virtual DbSet<Userresetpasswordcode> Userresetpasswordcodes { get; set; }

    public virtual DbSet<Usersubscription> Usersubscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Collectionlook>(entity =>
        {
            entity.HasKey(e => e.CollectionLookId).HasName("collectionlooks_pkey");

            entity.ToTable("collectionlooks");

            entity.Property(e => e.CollectionLookId).HasColumnName("collection_look_id");
            entity.Property(e => e.CollectionId).HasColumnName("collection_id");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.DateDeleted)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_deleted");
            entity.Property(e => e.DateModified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_modified");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.LookId).HasColumnName("look_id");
            entity.Property(e => e.Notes).HasColumnName("notes");

            entity.HasOne(d => d.Collection).WithMany(p => p.Collectionlooks)
                .HasForeignKey(d => d.CollectionId)
                .HasConstraintName("collectionlooks_collection_id_fkey");

            entity.HasOne(d => d.Look).WithMany(p => p.Collectionlooks)
                .HasForeignKey(d => d.LookId)
                .HasConstraintName("collectionlooks_look_id_fkey");
        });

        modelBuilder.Entity<Favoritecollection>(entity =>
        {
            entity.HasKey(e => e.CollectionId).HasName("favoritecollections_pkey");

            entity.ToTable("favoritecollections");

            entity.Property(e => e.CollectionId).HasColumnName("collection_id");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.DateDeleted)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_deleted");
            entity.Property(e => e.DateModified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_modified");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsPrivate)
                .HasDefaultValue(false)
                .HasColumnName("is_private");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Favoritecollections)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("favoritecollections_user_id_fkey");
        });

        modelBuilder.Entity<Look>(entity =>
        {
            entity.HasKey(e => e.LookId).HasName("looks_pkey");

            entity.ToTable("looks");

            entity.Property(e => e.LookId).HasColumnName("look_id");
            entity.Property(e => e.AvgRating)
                .HasPrecision(3, 2)
                .HasColumnName("avg_rating");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.DateDeleted)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_deleted");
            entity.Property(e => e.DateModified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_modified");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsPublic)
                .HasDefaultValue(true)
                .HasColumnName("is_public");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ThumbnailUrl)
                .HasMaxLength(255)
                .HasColumnName("thumbnail_url");
            entity.Property(e => e.TotalTransfers)
                .HasDefaultValue(0)
                .HasColumnName("total_transfers");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Looks)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("looks_created_by_fkey");
        });

        modelBuilder.Entity<Lookproduct>(entity =>
        {
            entity.HasKey(e => e.LookProductId).HasName("lookproducts_pkey");

            entity.ToTable("lookproducts");

            entity.Property(e => e.LookProductId).HasColumnName("look_product_id");
            entity.Property(e => e.ApplicationArea)
                .HasMaxLength(50)
                .HasColumnName("application_area");
            entity.Property(e => e.ApplicationOrder).HasColumnName("application_order");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.DateDeleted)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_deleted");
            entity.Property(e => e.DateModified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_modified");
            entity.Property(e => e.Intensity)
                .HasPrecision(3, 2)
                .HasColumnName("intensity");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.LookId).HasColumnName("look_id");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Look).WithMany(p => p.Lookproducts)
                .HasForeignKey(d => d.LookId)
                .HasConstraintName("lookproducts_look_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Lookproducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("lookproducts_product_id_fkey");
        });

        modelBuilder.Entity<Makeupproduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("makeupproducts_pkey");

            entity.ToTable("makeupproducts");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Brand)
                .HasMaxLength(100)
                .HasColumnName("brand");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.ColorCode)
                .HasMaxLength(7)
                .HasColumnName("color_code");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.DateDeleted)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_deleted");
            entity.Property(e => e.DateModified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_modified");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsVerified)
                .HasDefaultValue(false)
                .HasColumnName("is_verified");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("reviews_pkey");

            entity.ToTable("reviews");

            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.DateDeleted)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_deleted");
            entity.Property(e => e.DateModified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_modified");
            entity.Property(e => e.HelpfulVotes)
                .HasDefaultValue(0)
                .HasColumnName("helpful_votes");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.LookId).HasColumnName("look_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Look).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.LookId)
                .HasConstraintName("reviews_look_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("reviews_user_id_fkey");
        });

        modelBuilder.Entity<Subscriptiontier>(entity =>
        {
            entity.HasKey(e => e.TierId).HasName("subscriptiontiers_pkey");

            entity.ToTable("subscriptiontiers");

            entity.Property(e => e.TierId).HasColumnName("tier_id");
            entity.Property(e => e.AllowsCommercialUse)
                .HasDefaultValue(false)
                .HasColumnName("allows_commercial_use");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.DateDeleted)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_deleted");
            entity.Property(e => e.DateModified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_modified");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DurationDays).HasColumnName("duration_days");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.MaxCollections).HasColumnName("max_collections");
            entity.Property(e => e.MaxTransfers).HasColumnName("max_transfers");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.PriorityProcessing)
                .HasDefaultValue(false)
                .HasColumnName("priority_processing");
        });

        modelBuilder.Entity<Transfer>(entity =>
        {
            entity.HasKey(e => e.TransferId).HasName("transfers_pkey");

            entity.ToTable("transfers");

            entity.Property(e => e.TransferId).HasColumnName("transfer_id");
            entity.Property(e => e.AiModelVersion)
                .HasMaxLength(50)
                .HasColumnName("ai_model_version");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.DateDeleted)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_deleted");
            entity.Property(e => e.DateModified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_modified");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.LookId).HasColumnName("look_id");
            entity.Property(e => e.ProcessingTime)
                .HasPrecision(5, 2)
                .HasColumnName("processing_time");
            entity.Property(e => e.ResultImageUrl)
                .HasMaxLength(255)
                .HasColumnName("result_image_url");
            entity.Property(e => e.SourceImageUrl)
                .HasMaxLength(255)
                .HasColumnName("source_image_url");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Look).WithMany(p => p.Transfers)
                .HasForeignKey(d => d.LookId)
                .HasConstraintName("transfers_look_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Transfers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("transfers_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.DateDeleted)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_deleted");
            entity.Property(e => e.DateModified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_modified");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.LastLogin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_login");
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("password_hash");
            entity.Property(e => e.PasswordSalt)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnName("password_salt");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .HasColumnName("phone_number");
            entity.Property(e => e.ProfileImageUrl)
                .HasMaxLength(255)
                .HasColumnName("profile_image_url");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(64)
                .HasColumnName("refresh_token");
            entity.Property(e => e.RefreshTokenExpiry)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("refresh_token_expiry");
        });

        modelBuilder.Entity<Userclaim>(entity =>
        {
            entity.HasKey(e => e.ClaimId).HasName("userclaims_pk");

            entity.ToTable("userclaims");

            entity.Property(e => e.ClaimId)
                .ValueGeneratedNever()
                .HasColumnName("claim_id");
            entity.Property(e => e.ClaimType)
                .IsRequired()
                .HasMaxLength(64)
                .HasDefaultValueSql("'UNKNOWN'::character varying")
                .HasColumnName("claim_type");
            entity.Property(e => e.ClaimValue)
                .IsRequired()
                .HasMaxLength(64)
                .HasDefaultValueSql("'UNKNOWN'::character varying")
                .HasColumnName("claim_value");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Userclaims)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_id");
        });

        modelBuilder.Entity<Userpreference>(entity =>
        {
            entity.HasKey(e => e.PreferenceId).HasName("userpreferences_pkey");

            entity.ToTable("userpreferences");

            entity.HasIndex(e => e.UserId, "userpreferences_user_id_key").IsUnique();

            entity.Property(e => e.PreferenceId).HasColumnName("preference_id");
            entity.Property(e => e.AiEnhancementLevel)
                .HasDefaultValue(5)
                .HasColumnName("ai_enhancement_level");
            entity.Property(e => e.Allergies).HasColumnName("allergies");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.DateDeleted)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_deleted");
            entity.Property(e => e.DateModified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_modified");
            entity.Property(e => e.EyeColor)
                .HasMaxLength(50)
                .HasColumnName("eye_color");
            entity.Property(e => e.FaceShape)
                .HasMaxLength(50)
                .HasColumnName("face_shape");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.PreferredBrands).HasColumnName("preferred_brands");
            entity.Property(e => e.SkinTone)
                .HasMaxLength(50)
                .HasColumnName("skin_tone");
            entity.Property(e => e.SkinType)
                .HasMaxLength(50)
                .HasColumnName("skin_type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.Userpreference)
                .HasForeignKey<Userpreference>(d => d.UserId)
                .HasConstraintName("userpreferences_user_id_fkey");
        });

        modelBuilder.Entity<Userresetpasswordcode>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("userresetpasswordcodes_pkey");

            entity.ToTable("userresetpasswordcodes");

            entity.Property(e => e.Email)
                .HasMaxLength(128)
                .HasColumnName("email");
            entity.Property(e => e.CodeHash)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("code_hash");
            entity.Property(e => e.Expire)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expire");
        });

        modelBuilder.Entity<Usersubscription>(entity =>
        {
            entity.HasKey(e => e.UserSubscriptionId).HasName("usersubscriptions_pkey");

            entity.ToTable("usersubscriptions");

            entity.Property(e => e.UserSubscriptionId).HasColumnName("user_subscription_id");
            entity.Property(e => e.AutoRenew)
                .HasDefaultValue(true)
                .HasColumnName("auto_renew");
            entity.Property(e => e.CancellationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("cancellation_date");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_created");
            entity.Property(e => e.DateDeleted)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_deleted");
            entity.Property(e => e.DateModified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_modified");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_date");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.LastBillingDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_billing_date");
            entity.Property(e => e.NextBillingDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("next_billing_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("payment_method");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_date");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.TierId).HasColumnName("tier_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Tier).WithMany(p => p.Usersubscriptions)
                .HasForeignKey(d => d.TierId)
                .HasConstraintName("usersubscriptions_tier_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Usersubscriptions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("usersubscriptions_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}