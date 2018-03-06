using System.ComponentModel.DataAnnotations;

namespace MultiPART.Models.ViewModel
{
    public class UserProjectViewModel
    {
        [Required]
        public string UserName { get; set; }
        
        [Display(Name="Project Name")]
        public string Project { get; set; }

        [Display(Name="Associated Research Group")]
        public string Researchgroup { get; set; }

        [Display(Name="Position")]
        public string Role { get; set; }

        [Display(Name = "Project Status")]
        public string ProjectStatus { get; set; }

        public int ProjectID { get; set; }
    }
}