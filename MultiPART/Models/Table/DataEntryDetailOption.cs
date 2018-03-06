using System.ComponentModel.DataAnnotations.Schema;
using MultiPART.Models.LookupTable;

namespace MultiPART.Models.Table
{
    [Table("DataEntryDetailOption")]
    public class DataEntryDetailOption : DataEntryDetail
    {
       //[ForeignKey("DataEntryOption")]
        public int DataEntryOptionID { get; set; }

        public virtual DataEntryOption DataEntryOption { get; set; }
    }
}