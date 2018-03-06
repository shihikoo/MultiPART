using MultiPART.Models.Table;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPART.Models.LookupTable
{
    public class DataEntryField : ISoftDeletable
    {
        public DataEntryField()
        {
            DataEntryDesigns = new HashSet<DataEntryDesign>();
            DataEntryOptions = new HashSet<DataEntryOption>();

            Status = "Current";
        }

        public int DataEntryFieldID { get; set; }

        public string DataEntryFieldName { get; set; }

        [ForeignKey("FieldType")]
        public int FieldTypeID { get; set; }

        //[ForeignKey("ProcedurePurposeOrType")]
        //public int ProcedurePurposeOrTypeID { get; set; }

        public string Status { get; set; }

        //virtual
        public virtual ICollection<DataEntryDesign> DataEntryDesigns { get; set; }

        public virtual ICollection<DataEntryOption> DataEntryOptions { get; set; }

        public virtual Option FieldType { get; set; }

        //public virtual Option ProcedurePurposeOrType { get; set; }
    }
}