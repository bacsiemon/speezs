﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace speezs.DataAccess.Models;

[Table("userclaims")]
public partial class Userclaim
{
    [Column("user_id")]
    public int UserId { get; set; }

    [Key]
    [Column("claim_id")]
    public int ClaimId { get; set; }

    [Required]
    [Column("claim_type")]
    [StringLength(64)]
    public string ClaimType { get; set; }

    [Required]
    [Column("claim_value")]
    [StringLength(64)]
    public string ClaimValue { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Userclaims")]
    public virtual User User { get; set; }
}