using System.ComponentModel.DataAnnotations;

namespace MultiPART.Models.ViewModel
{
    public class InstitutionViewModel
    {
        public int InstitutionID { get; set; }

        [Required]
        [Display(Name = "Institution")]
        public string InstitutionName { get; set; }

        public int CountryCountryID { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; }

    }
}