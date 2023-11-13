﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SolarMP.Models
{
    public partial class Survey
    {
        public Survey()
        {
            ConstructionContract = new HashSet<ConstructionContract>();
        }

        [Key]
        [Column("surveyId")]
        [StringLength(16)]
        [Unicode(false)]
        public string SurveyId { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("note")]
        public string Note { get; set; }
        [Column("staffId")]
        [StringLength(16)]
        [Unicode(false)]
        public string StaffId { get; set; }
        [Column("status")]
        public bool Status { get; set; }
        [Column("requestId")]
        [StringLength(16)]
        [Unicode(false)]
        public string RequestId { get; set; }

        [ForeignKey("RequestId")]
        [InverseProperty("Survey")]
        public virtual Request Request { get; set; }
        [ForeignKey("StaffId")]
        [InverseProperty("Survey")]
        public virtual Account Staff { get; set; }
        [InverseProperty("Survey")]
        public virtual ICollection<ConstructionContract> ConstructionContract { get; set; }
    }
}