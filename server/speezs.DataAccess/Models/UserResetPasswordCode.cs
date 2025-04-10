﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace speezs.DataAccess.Models;

[Table("userresetpasswordcodes")]
public partial class Userresetpasswordcode
{
    [Key]
    [Column("email")]
    [StringLength(128)]
    public string Email { get; set; }

    [Required]
    [Column("code_hash")]
    [StringLength(256)]
    public string CodeHash { get; set; }

    [Column("expire", TypeName = "timestamp without time zone")]
    public DateTime Expire { get; set; }
}