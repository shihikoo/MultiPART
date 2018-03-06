using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPART.Models.Table
{
    [Table("DataEntryDetailValue")]
    public class DataEntryDetailValue : DataEntryDetail
    {
        public string Value { get; set; }
    }
}