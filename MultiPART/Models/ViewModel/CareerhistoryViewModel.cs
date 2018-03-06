using System;
using System.ComponentModel.DataAnnotations;

namespace MultiPART.Models.ViewModel
{
    public class CareerhistoryViewModel
    {
        public int CareerhistoryID { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        [Required]
        [Display(Name = "Institution")]
        public int InstitutionID { get; set; }

        [Display(Name = "Institution Name")]
        public string InstitutionName { get; set; }

        [Display(Name = "Start Date ")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.DateTime)]
        [Display(Name="End Date (Leave it blank if currently emploied)")]
        public DateTime? EndTime { get; set; }

        public string Position { get; set; }
    }
}