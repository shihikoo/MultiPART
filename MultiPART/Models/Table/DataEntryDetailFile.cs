using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPART.Models.Table
{
    [Table("DataEntryDetailFile")]
    public class DataEntryDetailFile : DataEntryDetail
    {
        //[ForeignKey("File")]
        public int FileID { get; set; }

        public virtual File File { get; set; }
    }
}