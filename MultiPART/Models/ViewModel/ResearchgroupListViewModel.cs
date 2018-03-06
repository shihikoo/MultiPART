using System;
using System.ComponentModel.DataAnnotations;

namespace MultiPART.Models.ViewModel
{
    public class ResearchgroupListViewModel
    {

        [Display(Name = "Research Group")]
        public int ResearchgroupID { get; set; }

        [Required]
        [Display(Name = "Research Group")]
        public string ResearchgroupName { get; set; }

        [Display(Name = "Institution")]
        public string InstitutionName { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Registration Date")]
        public DateTimeOffset CreatedOn { get; set; }


    }
}