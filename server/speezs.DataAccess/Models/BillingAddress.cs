﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace speezs.DataAccess.Models;

[Table("billing_address")]
public partial class BillingAddress
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("first_name")]
    [StringLength(20)]
    public string FirstName { get; set; }

    [Column("last_name")]
    [StringLength(20)]
    public string LastName { get; set; }

    [Column("country")]
    [StringLength(30)]
    public string Country { get; set; }

    [Column("city")]
    [StringLength(50)]
    public string City { get; set; }

    [Column("zip_code")]
    [StringLength(10)]
    public string ZipCode { get; set; }

    [Column("address")]
    [StringLength(100)]
    public string Address { get; set; }

    [InverseProperty("BillingAddress")]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    [ForeignKey("UserId")]
    [InverseProperty("BillingAddresses")]
    public virtual User User { get; set; }
}