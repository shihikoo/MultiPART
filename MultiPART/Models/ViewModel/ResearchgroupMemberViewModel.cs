using System;
using System.ComponentModel.DataAnnotations;

namespace MultiPART.Models.ViewModel
{
    public class ResearchgroupMemberViewModel
    {
        [Display(Name = "User In Researchgroup ")]
        public int UserInResearchgroupID { get; set; }

        [Display(Name = "Research Group")]
        public int ResearchgroupID { get; set; }

        [Display(Name = "Institution")]
        public int InstitutionID { get; set; }

        [Display(Name = "New Group Member")]
        public int UserID { get; set; }

        [Display(Name = "Group Member")]
        public string UserName { get; set; }

        [Display(Name = "Member Role")]
        public int  UserRoleInResearchgroupID { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time (Ignore If Ongoing)")]
        public DateTime? EndTime { get; set; }
    }
}