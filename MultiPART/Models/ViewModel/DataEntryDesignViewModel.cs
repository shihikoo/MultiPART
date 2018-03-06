using System.ComponentModel;

namespace MultiPART.Models.ViewModel
{
    public class DataEntryDesignViewModel
    {
        
        [DisplayName("Selected")]
        public bool Selected { get; set; }

        [DisplayName("Data Entry Design")]
        public int DataEntryDesignID { get; set; }

        [DisplayName("Procedure ID")]
        public int ProcedureID { get; set; }

        [DisplayName("Data Entry Field")]
        public int DataEntryFieldID { get; set; }

        [DisplayName("Form Field")]
        public string DataEntryFieldName { get; set; }

        [DisplayName("Mandatory?")]
        public bool Mandatory { get; set; }

        [DisplayName("Multiple?")]
        public bool Multiple { get; set; }

    }
}