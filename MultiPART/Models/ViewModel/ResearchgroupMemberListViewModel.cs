using System;
using System.ComponentModel.DataAnnotations;

namespace MultiPART.Models.ViewModel
{
    public class ResearchgroupMemberListViewModel
    {
        public int UserInResearchgroupID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Role")]
        public string UserRoleInResearchgroup { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "End Time")]
        public DateTime? EndTime { get; set; }

    }
}