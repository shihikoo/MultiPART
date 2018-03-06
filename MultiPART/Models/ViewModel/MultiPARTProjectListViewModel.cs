using System;
using System.ComponentModel.DataAnnotations;

namespace MultiPART.Models.ViewModel
{
    public class MultiPARTProjectListViewModel
    {

        public int MultiPARTProjectID { get; set; }

        [Display(Name = "Project Name")]
        public string MultiPARTProjectName { get; set; }

        [Display(Name = "Objectives")]
        public string Objectives { get; set; }

        public string Logo { get; set; }
        [Display(Name = "Created By")]
        public string Creator { get; set; }


        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Start Date")]
        public DateTime ProjectStartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Expected Completion Date")]
        public DateTime ProjectCompletionDateExpected { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Completion Date")]
        public DateTime? ProjectComletionDate { get; set; }

    }
}