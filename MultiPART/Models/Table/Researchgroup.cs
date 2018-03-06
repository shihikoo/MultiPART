using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MultiPART.Models.LinkTable;
using MultiPART.Models.Table;
using System.Web.Security;

namespace MultiPART.Models
{
    public class Researchgroup : ISoftDeletable
    {
        public Researchgroup()
        {
            ResearchgroupInMultiPARTProjects = new HashSet<ResearchgroupInMultiPARTProject>();
            UserInResearchgroups = new HashSet<UserInResearchgroup>();
            ResearchgroupCohortAssignments = new HashSet<ResearchgroupCohortAssignment>();
            Animals = new HashSet<Animal>();

            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;

            AnimalHusbandries = new HashSet<AnimalHusbandry>();
            //AsepticTechniques = new HashSet<AsepticTechnique>();

            Status = "Current";
        }

        [Key]
        public int ResearchgroupID { get; set; }

        public string ResearchgroupName { get; set; }

        [ForeignKey("Institutions")]
        public int InstitutionInstitutionID { get; set; }

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

        public virtual Institution Institutions { get; set; }

        public virtual ICollection<Animal> Animals { get; set; }        

        public virtual ICollection<ResearchgroupCohortAssignment> ResearchgroupCohortAssignments { get; set; }    
    
        public virtual ICollection<UserInResearchgroup>  UserInResearchgroups{ get; set; }

        public virtual ICollection<ResearchgroupInMultiPARTProject> ResearchgroupInMultiPARTProjects { get; set; }

        public virtual ICollection<AnimalHusbandry> AnimalHusbandries { get; set; }


    }
}