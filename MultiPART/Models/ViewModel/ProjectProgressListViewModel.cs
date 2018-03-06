using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiPART.Models.ViewModel
{
    public class ProjectProgressListViewModel
    {
        public string ProjectName { get; set; }

        public int ProjectID { get; set; }

        public IList<ProjectProgressViewModel> Progress { get; set; }


    }
}