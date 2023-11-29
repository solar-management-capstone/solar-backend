﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SolarMP.Models
{
    public partial class Process
    {
        public Process()
        {
            Image = new HashSet<Image>();
        }

        [Key]
        [Column("processId")]
        [StringLength(16)]
        [Unicode(false)]
        public string ProcessId { get; set; }
        [Required]
        [Column("title")]
        public string Title { get; set; }
        [Required]
        [Column("description")]
        public string Description { get; set; }
        [Column("status")]
        public bool Status { get; set; }
        [Column("startDate", TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column("endDate", TypeName = "datetime")]
        public DateTime EndDate { get; set; }
        [Column("createAt", TypeName = "datetime")]
        public DateTime CreateAt { get; set; }
        [Required]
        [Column("contractId")]
        [StringLength(16)]
        [Unicode(false)]
        public string ContractId { get; set; }

        [ForeignKey("ContractId")]
        [InverseProperty("Process")]
        public virtual ConstructionContract Contract { get; set; }
        [InverseProperty("Process")]
        public virtual ICollection<Image> Image { get; set; }
    }
}