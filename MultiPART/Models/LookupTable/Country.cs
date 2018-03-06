using System.Collections.Generic;
using MultiPART.Models.Table;

namespace MultiPART.Models.LookupTable
{
    public class Country : ISoftDeletable
    {
        public Country()
        {
            Institutions = new HashSet<Institution>();
            Status = "Current";
        }

        public int CountryID { get; set; }

        public string CountryName { get; set; }

        public string CountryCode { get; set; }

        public string Status { get; set; }

        public virtual ICollection<Institution> Institutions { get; set; }
    }
}