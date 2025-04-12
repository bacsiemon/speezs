﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace speezs.DataAccess.Models;

[Table("users")]
[Index("Email", Name = "users_email_key", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [Required]
    [Column("email")]
    [StringLength(128)]
    public string Email { get; set; }

    [Required]
    [Column("password_hash")]
    [StringLength(256)]
    public string PasswordHash { get; set; }

    [Required]
    [Column("password_salt")]
    [StringLength(64)]
    public string PasswordSalt { get; set; }

    [Column("phone_number")]
    [StringLength(10)]
    public string PhoneNumber { get; set; }

    [Column("full_name")]
    [StringLength(100)]
    public string FullName { get; set; }

    [Column("profile_image_url")]
    [StringLength(255)]
    public string ProfileImageUrl { get; set; }

    [Column("last_login", TypeName = "timestamp without time zone")]
    public DateTime? LastLogin { get; set; }

    [Column("is_deleted")]
    public bool? IsDeleted { get; set; }

    [Column("date_created", TypeName = "timestamp without time zone")]
    public DateTime? DateCreated { get; set; }

    [Column("date_modified", TypeName = "timestamp without time zone")]
    public DateTime? DateModified { get; set; }

    [Column("date_deleted", TypeName = "timestamp without time zone")]
    public DateTime? DateDeleted { get; set; }

    [Column("refresh_token")]
    [StringLength(64)]
    public string RefreshToken { get; set; }

    [Column("refresh_token_expiry", TypeName = "timestamp without time zone")]
    public DateTime? RefreshTokenExpiry { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Favoritecollection> Favoritecollections { get; set; } = new List<Favoritecollection>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Look> Looks { get; set; } = new List<Look>();

    [InverseProperty("User")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("User")]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    [InverseProperty("User")]
    public virtual ICollection<Transfer> Transfers { get; set; } = new List<Transfer>();

    [InverseProperty("User")]
    public virtual ICollection<Userclaim> Userclaims { get; set; } = new List<Userclaim>();

    [InverseProperty("User")]
    public virtual Userpreference Userpreference { get; set; }

    [InverseProperty("User")]
    public virtual Userrole Userrole { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Usersubscription> Usersubscriptions { get; set; } = new List<Usersubscription>();
}