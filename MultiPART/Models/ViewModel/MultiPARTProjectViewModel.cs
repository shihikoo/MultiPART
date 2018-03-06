using System;
using System.ComponentModel.DataAnnotations;

namespace MultiPART.Models.ViewModel
{
    public class MultiPARTProjectViewModel
    {

        public int MultiPARTProjectID { get; set; }

        [Required]
        [Display(Name = "Project Name *")]
        public string MultiPARTProjectName { get; set; }
        [Required]
        [Display(Name = "Objectives *")]
        public string Objectives { get; set; }

        [Display(Name = "Background")]
        public string Background { get; set; }

        [Display(Name = "Ethical Statement")]
        public string EthicalStatement { get; set; }

        [Display(Name = "Analysis Plan")]
        public string AnalysisPlan { get; set; }
        
        [Display(Name = "Funding")]
        public string Funding { get; set; }

        public string Logo { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Start Date *")]
        public DateTime ProjectStartDate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Expected Completion Date*")]
        public DateTime ProjectCompletionDateExpected { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Actual Completion Date")]
        public DateTime? ProjectComletionDate { get; set; }

    }
}