using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MultiPART.Models.ViewModel
{
    public class ProcedureViewModel
    {
        public int ProcedureID { get; set; }

        public string ProjectName { get; set; }

        public int MultiPARTProjectID { get; set; }

        [DisplayName("Procedure Label")]
        [Required]
        public string ProcedureLabel { get; set; }

        [DisplayName("Procedure Type")]
        public int? ProcedureTypeID { get; set; }

        [DisplayName("Procedure Purpose")]
        public int ProcedurePurposeID { get; set; }

        public string ProcedurePurpose { get; set; }

        [DisplayName("Notes")]
        public string Details { get; set; }

        public IList<AdministrationViewModel> Administrations { get; set; }

        //[DisplayName("Administration Type ")]
        //public int? AdministrationTypeID { get; set; }
        public bool Entered { get; set; }

         public SelectList ProcedureTypeList { get; set; }

    }
}