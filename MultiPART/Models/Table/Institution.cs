using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;
using MultiPART.Models.LinkTable;
using MultiPART.Models.LookupTable;

namespace MultiPART.Models.Table
{
    public class Institution : ISoftDeletable
    {

        public Institution()
        {
            Careerhistories = new HashSet<Careerhistory>();
            Researchgroups = new HashSet<Researchgroup>();
            Status = "Current";
            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;

        }

        [Key]
        public int InstitutionID { get; set; }

        public string InstitutionName { get; set; }

         [ForeignKey("Countries")]
        public int CountryCountryID { get; set; }

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

        public virtual Country Countries { get; set; }

        public virtual ICollection<Careerhistory> Careerhistories { get; set; }
        public virtual ICollection<Researchgroup> Researchgroups { get; set; }
    }
}