using System.Collections.Generic;
using MultiPART.Models.LinkTable;
using System.ComponentModel.DataAnnotations.Schema;
using MultiPART.Models.Table;


namespace MultiPART.Models.LookupTable
{
    public class Option : ISoftDeletable
    {
        public Option()
        {
            UserInResearchgroups = new HashSet<UserInResearchgroup>();
            UserProjectAssignments = new HashSet<UserProjectAssignment>();
            ResearchgroupInMultiPARTProjects = new HashSet<ResearchgroupInMultiPARTProject>();
            Procedures = new HashSet<Procedure>();
            Cohorts = new HashSet<Cohort>();
            Animals = new HashSet<Animal>();
            DataEntryFields = new HashSet<DataEntryField>();
            AnimalHusbandryFields = new HashSet<AnimalHusbandryField>();
            ProcedureDetailOptionFields = new HashSet<ProcedureDetailOptionField>();
            Status = "Current";
        }

        public int OptionID { get; set; }

        [ForeignKey("OptionFields")]
        public int OptionFieldOptionFieldID { get; set; }

        public string OptionValue { get; set; }

        public string Status { get; set; }

        public virtual OptionField OptionFields { get; set; }

        public virtual ICollection<ResearchgroupInMultiPARTProject> ResearchgroupInMultiPARTProjects { get; set; }

        public virtual ICollection<UserInResearchgroup> UserInResearchgroups { get; set; }

        public virtual ICollection<UserProjectAssignment> UserProjectAssignments { get; set; }

        public virtual ICollection<Procedure> Procedures { get; set; }

        public virtual ICollection<Cohort> Cohorts { get; set; }

        public virtual ICollection<Animal> Animals { get; set; }

        public virtual ICollection<DataEntryField> DataEntryFields { get; set; }

        public virtual ICollection<AnimalHusbandryField> AnimalHusbandryFields { get; set; }

        public virtual ICollection<ProcedureDetailOptionField> ProcedureDetailOptionFields { get; set; }
    }
}