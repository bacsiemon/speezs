﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace speezs.DataAccess.Models;

[Table("usersubscriptions")]
public partial class Usersubscription
{
    [Key]
    [Column("user_subscription_id")]
    public int UserSubscriptionId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("tier_id")]
    public int? TierId { get; set; }

    [Column("start_date", TypeName = "timestamp without time zone")]
    public DateTime StartDate { get; set; }

    [Column("end_date", TypeName = "timestamp without time zone")]
    public DateTime EndDate { get; set; }

    [Required]
    [Column("status")]
    [StringLength(20)]
    public string Status { get; set; }

    [Column("payment_method")]
    [StringLength(50)]
    public string PaymentMethod { get; set; }

    [Column("auto_renew")]
    public bool? AutoRenew { get; set; }

    [Column("last_billing_date", TypeName = "timestamp without time zone")]
    public DateTime? LastBillingDate { get; set; }

    [Column("next_billing_date", TypeName = "timestamp without time zone")]
    public DateTime? NextBillingDate { get; set; }

    [Column("cancellation_date", TypeName = "timestamp without time zone")]
    public DateTime? CancellationDate { get; set; }

    [Column("is_deleted")]
    public bool? IsDeleted { get; set; }

    [Column("date_created", TypeName = "timestamp without time zone")]
    public DateTime? DateCreated { get; set; }

    [Column("date_modified", TypeName = "timestamp without time zone")]
    public DateTime? DateModified { get; set; }

    [Column("date_deleted", TypeName = "timestamp without time zone")]
    public DateTime? DateDeleted { get; set; }

    [Column("transfers_left")]
    public int? TransfersLeft { get; set; }

    [ForeignKey("TierId")]
    [InverseProperty("Usersubscriptions")]
    public virtual Subscriptiontier Tier { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Usersubscriptions")]
    public virtual User User { get; set; }
}