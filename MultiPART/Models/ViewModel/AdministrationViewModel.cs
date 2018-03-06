using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiPART.Models.ViewModel
{
    public class AdministrationViewModel
    {
        public int AdministrationID { get; set; }

        public int ProcedureID { get; set; }

        public int ProjectID { get; set; }

        [DisplayName("Start Time")]
        public float StartTime { get; set; }

        [DisplayName("End Time")]
        public float EndTime { get; set; }

        [DisplayName("Unit")]
        public int unitID { get; set; }

        public SelectList unitList { get; set; }

        public bool Entered { get; set; }



    }
}