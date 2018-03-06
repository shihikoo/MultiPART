using System.Collections.Generic;

namespace MultiPART.Models.LookupTable
{
    public class SIUnit
    {
        public SIUnit()
        {
          
            Status = "Current";
            Units = new HashSet<Unit>();
        }

        public int SIUnitID { get; set; }

        public string QuantityName { get; set; }

        public string UnitName { get; set; }

        public string UnitSymbol { get; set; }

        public string Status { get; set; }


        public ICollection<Unit> Units{get;set;}
    }
}