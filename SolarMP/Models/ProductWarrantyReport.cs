﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SolarMP.Models
{
    public partial class ProductWarrantyReport
    {
        [Key]
        [Column("productId")]
        [StringLength(16)]
        [Unicode(false)]
        public string ProductId { get; set; }
        [Key]
        [Column("warrantyId")]
        [StringLength(16)]
        [Unicode(false)]
        public string WarrantyId { get; set; }
        [Column("amountofDamageProduct", TypeName = "decimal(10, 2)")]
        public decimal? AmountofDamageProduct { get; set; }
        [Column("status")]
        public bool Status { get; set; }
        [Column("doWarranty")]
        public string DoWarranty { get; set; }

        [ForeignKey("ProductId")]
        [InverseProperty("ProductWarrantyReport")]
        public virtual Product Product { get; set; }
        [ForeignKey("WarrantyId")]
        [InverseProperty("ProductWarrantyReport")]
        public virtual WarrantyReport Warranty { get; set; }
    }
}