using MultiPART.Models.Table;
using System.Collections.Generic;

namespace MultiPART.Models.LookupTable
{
    public class AnimalSupplier : ISoftDeletable
    {
        public AnimalSupplier()
        {
            Cohorts = new HashSet<Cohort>();

            Status = "Current";
        }

        public int AnimalSupplierID { get; set; }

        public string AnimalSupplierName { get; set; }

        public string Status { get; set; }

        public virtual ICollection<Cohort> Cohorts { get; set; }

    }
}