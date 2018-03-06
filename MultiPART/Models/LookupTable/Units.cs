using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPART.Models.LookupTable
{
    public class Unit
    {
        public int UnitID { get; set; }

        public string UnitName { get; set; }

        public string UnitSymbol { get; set; }

        public float ConversionFactor { get; set; }

        [ForeignKey ("SIUnite")]
        public int SIID { get; set; }

        public string Status { get; set; }

        public virtual SIUnit SIUnite { get; set; }

    }
}