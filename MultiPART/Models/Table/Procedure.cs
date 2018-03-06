using MultiPART.Models.LinkTable;
using MultiPART.Models.LookupTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;

namespace MultiPART.Models.Table
{
    public class Procedure : ISoftDeletable
    {
        public Procedure()
        {
            CohortProcedureAssignments = new HashSet<CohortProcedureAssignment>();
            ProcedureDetails = new HashSet<ProcedureDetail>();
            DataEntryDesigns = new HashSet<DataEntryDesign>();
            Administrations = new HashSet<Administration>();

            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;

            Status = "Current";
        }

        [Key]
        public int ProcedureID { get; set; }

        [ForeignKey("MultiPARTProjects")]
        public int MultiPARTProjectMultiPARTProjectID { get; set; }

        [ForeignKey("OptionsProcedureType")]
        public int? ProcedureTypeID { get; set; }

        [ForeignKey("OptionsProcedurePurpose")]
        public int ProcedurePurposeID { get; set; }

        //[ForeignKey("OptionsAdministrationType")]
        //public int? AdministrationTypeID { get; set; }
        public string ProcedureLabel { get; set; }

        public string Details { get; set; }

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
    
        public virtual Option OptionsProcedureType { get; set; }

        public virtual Option OptionsProcedurePurpose { get; set; }

        //public virtual Option OptionsAdministrationType { get; set; }
      
        //virtual collection
        //public virtual ICollection<AnimalProcedure> AnimalProcedures { get; set; }

        public virtual ICollection<CohortProcedureAssignment> CohortProcedureAssignments { get; set; }

        public virtual ICollection<ProcedureDetail> ProcedureDetails { get; set; }

        public virtual ICollection<DataEntryDesign> DataEntryDesigns { get; set; }

        public virtual ICollection<Administration> Administrations { get; set; }

    }
}