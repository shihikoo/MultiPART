using MultiPART.Models.Table;
using System.Collections.Generic;

namespace MultiPART.Models.LookupTable
{
    public class Strain : ISoftDeletable
    {
        public Strain()
        {
            Cohorts = new HashSet<Cohort>();
            AnimalHusbandries = new HashSet<AnimalHusbandry>();
            Status = "Current";
        }

        public int StrainID { get; set; }

        public string StrainName { get; set; }

        public int SpeciesID { get; set; }

        public string Status { get; set; }

        public virtual Species Species { get; set; }

        public virtual ICollection<Cohort> Cohorts { get; set; }
        public virtual ICollection<AnimalHusbandry> AnimalHusbandries { get; set; }

    }
}