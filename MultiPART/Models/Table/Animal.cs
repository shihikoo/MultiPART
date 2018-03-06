using MultiPART.Models.LinkTable;
using MultiPART.Models.LookupTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;

namespace MultiPART.Models.Table
{
    public class Animal :ISoftDeletable
    {
        public Animal()
        {
            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;
            Status = "Current";
        }

        [Key]
        public int AnimalID { get; set; }

        public string AnimalLabel { get; set; }

        // weight in grams
        public float Weight { get; set; }

        // age in days
        public int? Age { get; set; }

        [ForeignKey("OptionsSex")]
        public int? SexID { get; set; }

        [ForeignKey("Cohort")]
        public int CohortID { get; set; }

        [ForeignKey("Researchgroup")]
        public int ResearchgroupID { get; set; }

        public string Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public int LastUpdatedBy { get; set; }

        private DateTimeOffset? lastUpdatedOn;
        [Required]
        public DateTimeOffset LastUpdatedOn
        {
            get { return lastUpdatedOn ?? DateTimeOffset.Now; }
            set { lastUpdatedOn = value; }
        }

        //virtual
        //public virtual ICollection<AnimalProcedure> AnimalProcedures { get; set; }

        public virtual ICollection<AnimalAdministration> AnimalAdministrations { get; set; }

        public virtual Option OptionsSex { get; set; }

        public virtual Cohort Cohort { get; set; }

        public virtual Researchgroup Researchgroup { get; set; }

    }
}