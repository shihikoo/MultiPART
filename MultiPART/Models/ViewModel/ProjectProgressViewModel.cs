using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiPART.Models.ViewModel
{
    public class ProjectProgressViewModel
    {
        public string ResearchgroupName { get; set; }

        public int AssignedNumber { get; set; }

        public int CreatedNumber { get; set; }


        public int? DeathNumber { get; set; }

        public int? CompletedNumber { get; set; }

        public string WidthStyleCreation { get; set; }

        public string WidthStyleDeath { get; set; }

        public string WidthStyleComplete { get; set; }

    }
}