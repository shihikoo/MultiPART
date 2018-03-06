using System;
using System.ComponentModel.DataAnnotations;

namespace MultiPART.Models.ViewModel
{
    public class ResearchgroupInMultiPARTProjectViewModel
    {
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        public int ResearchgroupInMultiPARTProjectID { get; set; }

        [Display(Name = "Project")]
        public int MultiPARTProjectID { get; set; }

        [Display(Name = "Research Group")]
        [Required]
        public int ResearchgroupID { get; set; }

        [Display(Name = "Research Group Name")]
        public string ResearchgroupName { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Registration Date")]
        [Required]
        public DateTime? RegistrationDate { get; set; }

        [Required]
        [Display(Name = "Research Group Role")]
        public int ResearchgroupRoleinMultiPARTProjectID { get; set; }

        [Display(Name = "Research Group Role")]
        public string ResearchgroupRoleinMultiPARTProject { get; set; }

        public bool Editable { get; set; }
    }
}