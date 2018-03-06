using MultiPART.Models.LookupTable;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;

namespace MultiPART.Models.Table
{
    public class AnimalHusbandry:ISoftDeletable
    {
        public AnimalHusbandry()
        {
            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;
            Status = "Current";
        }

        [Key]
        public int AnimalHusbandryID { get; set; }

        [ForeignKey("MultiPARTProjects")]
        public int MultiPARTProjectMultiPARTProjectID { get; set; }

        [ForeignKey("Researchgroups")]
        public int ResearchgroupResearchgroupID { get; set; }

        [ForeignKey("Strains")]
        public int StrainID { get; set; }

        [ForeignKey("AnimalHusbandryFields")]
        public int AnimalHusbandryFieldID { get; set; }

        [ForeignKey("AnimalHusbandryOptions")]
        public int? AnimalHusbandryOptionID { get; set; }

        public string Value { get; set; }

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
        public virtual MultiPARTProject MultiPARTProjects { get; set; }

        public virtual Researchgroup Researchgroups { get; set; }

        public virtual Strain Strains { get; set; }

        public virtual AnimalHusbandryField AnimalHusbandryFields { get; set; }

        public virtual AnimalHusbandryOption AnimalHusbandryOptions { get; set; }

    }
}