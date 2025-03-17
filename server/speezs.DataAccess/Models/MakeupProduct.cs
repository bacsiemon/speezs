﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace speezs.DataAccess.Models;

public partial class MakeupProduct
{
    public int ProductId { get; set; }

    public string Name { get; set; }

    public string Brand { get; set; }

    public string Category { get; set; }

    public string ColorCode { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    public bool? IsVerified { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public DateTime? DateDeleted { get; set; }

    public virtual ICollection<LookProduct> Lookproducts { get; set; } = new List<LookProduct>();
}