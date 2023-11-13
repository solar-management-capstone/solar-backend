﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SolarMP.Models
{
    public partial class ConstructionContract
    {
        public ConstructionContract()
        {
            Acceptance = new HashSet<Acceptance>();
            Feedback = new HashSet<Feedback>();
            PaymentProcess = new HashSet<PaymentProcess>();
            Process = new HashSet<Process>();
            WarrantyReport = new HashSet<WarrantyReport>();
        }

        [Key]
        [Column("constructioncontractId")]
        [StringLength(16)]
        [Unicode(false)]
        public string ConstructioncontractId { get; set; }
        [Required]
        [Column("status")]
        [StringLength(50)]
        public string Status { get; set; }
        [Column("startdate", TypeName = "date")]
        public DateTime? Startdate { get; set; }
        [Column("enddate", TypeName = "date")]
        public DateTime? Enddate { get; set; }
        [Column("totalcost", TypeName = "decimal(16, 0)")]
        public decimal? Totalcost { get; set; }
        [Column("isConfirmed")]
        public bool? IsConfirmed { get; set; }
        [Column("imageFile")]
        public string ImageFile { get; set; }
        [Column("customerId")]
        [StringLength(16)]
        [Unicode(false)]
        public string CustomerId { get; set; }
        [Column("staffid")]
        [StringLength(16)]
        [Unicode(false)]
        public string Staffid { get; set; }
        [Column("packageId")]
        [StringLength(16)]
        [Unicode(false)]
        public string PackageId { get; set; }
        [Column("bracketId")]
        [StringLength(16)]
        [Unicode(false)]
        public string BracketId { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("surveyId")]
        [StringLength(16)]
        [Unicode(false)]
        public string SurveyId { get; set; }

        [ForeignKey("BracketId")]
        [InverseProperty("ConstructionContract")]
        public virtual Bracket Bracket { get; set; }
        [ForeignKey("CustomerId")]
        [InverseProperty("ConstructionContractCustomer")]
        public virtual Account Customer { get; set; }
        [ForeignKey("PackageId")]
        [InverseProperty("ConstructionContract")]
        public virtual Package Package { get; set; }
        [ForeignKey("Staffid")]
        [InverseProperty("ConstructionContractStaff")]
        public virtual Account Staff { get; set; }
        [ForeignKey("SurveyId")]
        [InverseProperty("ConstructionContract")]
        public virtual Survey Survey { get; set; }
        [InverseProperty("ConstructionContract")]
        public virtual ICollection<Acceptance> Acceptance { get; set; }
        [InverseProperty("ContructionContract")]
        public virtual ICollection<Feedback> Feedback { get; set; }
        [InverseProperty("ConstructionContract")]
        public virtual ICollection<PaymentProcess> PaymentProcess { get; set; }
        [InverseProperty("Contract")]
        public virtual ICollection<Process> Process { get; set; }
        [InverseProperty("Contract")]
        public virtual ICollection<WarrantyReport> WarrantyReport { get; set; }
    }
}