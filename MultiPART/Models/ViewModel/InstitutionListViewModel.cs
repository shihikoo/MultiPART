using System;
using System.ComponentModel.DataAnnotations;

namespace MultiPART.Models.ViewModel
{
    public class InstitutionListViewModel
    {
        public int InstitutionID { get; set; }

        [Required]
        [Display(Name = "Institution")]
        public string InstitutionName { get; set; }
        
        [Display(Name = "Country")]
        public string Country { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Registration Date")]
        public DateTimeOffset CreatedOn { get; set; }

        //[DisplayFormat(DataFormatString = "{0:d}")]
        //[Display(Name = "Updated Date")]
        //public DateTimeOffset LastUpdatedOn { get; set; }
    }
}