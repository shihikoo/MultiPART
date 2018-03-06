using System.ComponentModel;

namespace MultiPART.Models.ViewModel
{
    public class ProcedureListViewModel
    {
        public int ProcedureID { get; set; }

        [DisplayName("Procedure Label ")]
        public string ProcedureLabel { get; set; }

        public int MultiPARTProjectMultiPARTProjectID { get; set; }

        [DisplayName("Procedure Type ")]
        public string ProcedureType { get; set; }

        [DisplayName("Procedure Purpose")]
        public string ProcedurePurpose { get; set; }

        [DisplayName("Notes")]
        public string Details { get; set; }

        [DisplayName("Number Of Details")]
        public int NumberOfDetails { get; set; }

        [DisplayName("Number Of Administration")]
        public int NumberOfAdministration { get; set; }

        [DisplayName("Number Of Form Fields")]
        public int NumberOfFormField { get; set; }

    }
}