﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SolarMP.Models
{
    public partial class Bracket
    {
        public Bracket()
        {
            ConstructionContract = new HashSet<ConstructionContract>();
            Image = new HashSet<Image>();
        }

        [Key]
        [Column("bracketId")]
        [StringLength(16)]
        [Unicode(false)]
        public string BracketId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("price", TypeName = "decimal(16, 0)")]
        public decimal? Price { get; set; }
        [Column("material")]
        public string Material { get; set; }
        [Column("size", TypeName = "decimal(16, 0)")]
        public decimal? Size { get; set; }
        [Column("manufacturer")]
        public string Manufacturer { get; set; }
        [Column("status")]
        public bool Status { get; set; }
        [Column("createAt", TypeName = "datetime")]
        public DateTime? CreateAt { get; set; }

        [InverseProperty("Bracket")]
        public virtual ICollection<ConstructionContract> ConstructionContract { get; set; }
        [InverseProperty("Bracket")]
        public virtual ICollection<Image> Image { get; set; }
    }
}