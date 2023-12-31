﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SolarMP.Models
{
    public partial class Promotion
    {
        public Promotion()
        {
            Package = new HashSet<Package>();
        }

        [Key]
        [Column("promotionId")]
        [StringLength(16)]
        [Unicode(false)]
        public string PromotionId { get; set; }
        [Column("amount", TypeName = "decimal(18, 0)")]
        public decimal? Amount { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("startDate", TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column("endDate", TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [Column("createAt", TypeName = "datetime")]
        public DateTime? CreateAt { get; set; }
        [Column("status")]
        public bool Status { get; set; }

        [InverseProperty("Promotion")]
        public virtual ICollection<Package> Package { get; set; }
    }
}