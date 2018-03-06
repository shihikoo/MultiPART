using MultiPART.Models.Table;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPART.Models.LookupTable
{
    public class DataEntryOption : ISoftDeletable
    {

        public DataEntryOption()
        {
            Status = "Current";
            DataEntryDetailOptions = new HashSet<DataEntryDetailOption>();

        }

        public int DataEntryOptionID { get; set; }

        [ForeignKey("DataEntryField")]
        public int DataEntryFieldDataEntryFieldID { get; set; }

        public string DataEntryOptionName { get; set; }

        public string Status { get; set; }

        //virtual

        public virtual DataEntryField DataEntryField { get; set; }

        public virtual ICollection<DataEntryDetailOption> DataEntryDetailOptions { get; set; }
    }
}