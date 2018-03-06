using System.Collections.Generic;

namespace MultiPART.Models.LookupTable
{
    public class Species : ISoftDeletable
    {

        public Species()
        {
            Status = "Current";
            Strains = new HashSet<Strain>();
        }

        public int SpeciesID { get; set; }

        public string SpeciesName { get; set; }

        public string Status { get; set; }

        public ICollection<Strain> Strains { get; set; }
    }
}