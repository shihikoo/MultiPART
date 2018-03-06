using MultiPART.Models.LinkTable;
using MultiPART.Models.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPART.Models
{

    [Table("UserProfile")]
    public class UserProfile
    {
         public UserProfile()
        {
            Careerhistories = new HashSet<Careerhistory>();
            CreatedOn = DateTimeOffset.Now;
            Status = "Current";
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Username *")]
        public string UserName { get; set; }

         [Required]
         [Display(Name = "Forename *")]
        public string ForeName { get; set; }

         [Required]
         [Display(Name = "Surname *")]
        public string SurName { get; set; }

         [Required]
        public string Email { get; set; }
         
        [Display(Name = "Note")]
        public string Details { get; set; }

        public string Status { get; set; }

        [Display(Name = "Registration Date/Time")]
        public DateTimeOffset CreatedOn { get; set; }

        private DateTimeOffset? lastUpdatedOn;
        [Required]
        public DateTimeOffset LastUpdatedOn
        {
            get { return lastUpdatedOn ?? DateTimeOffset.Now; }
            set { lastUpdatedOn = value; }
        }

        public int? LastUpdatedBy { get; set; }

        public virtual ICollection<UserInResearchgroup> UserInResearchgroups { get; set; }
        public virtual ICollection<Careerhistory> Careerhistories { get; set; }
        public virtual ICollection<UserDataentryAssignment> UserDataentryAssignments { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username *")]
        [MinLength(1, ErrorMessage = "Username must be longer than 1 digit")]
        [RegularExpression(@"([a-zA-Z0-9.&'-@]+)", ErrorMessage = "Username should be alphanumeric with no space.")]
        //[Remote("ValidateUserName", "Record", ErrorMessage = "This Username is already in the record. ")]       
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password *")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password *")]
        [System.Web.Mvc.CompareAttribute("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Forename *")]
        [Required]
        public string ForeName { get; set; }

        [DisplayName("Surname *")]
        [Required]
        public string SurName { get; set; }

        [Required]
        //[DataType(DataType.EmailAddress,ErrorMessage = "Valid Message is required")]
        [EmailAddress]
        [Display(Name = "Email *")]
        public string Email { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Confirm Email *")]
        [System.Web.Mvc.CompareAttribute("Email", ErrorMessage = "The emails you've entered do not match.")]
        public string ConfirmEmail { get; set; }

        [DisplayName("Notes")]
        public string Details { get; set; }

        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }

 public class LostPasswordModel
    {
        [Required(ErrorMessage = "We need your email to send you a reset link!")]
        [Display(Name = "Your account email")]
        [EmailAddress(ErrorMessage = "Not a valid email--what are you trying to do here?")]
        [DataType(DataType.Password)]
        public string Email { get; set; }
    }

    public class ResetPasswordModel
    {
        [Required]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [System.Web.Mvc.CompareAttribute("Password", ErrorMessage = "New password and confirmation does not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ReturnToken { get; set; }
    }

    public class UserViewModel
    {
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Forename")]
        public string ForeName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string SurName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Note")]
        public string Details { get; set; }

        [Display(Name="Registration Date")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Roles")]
        public string Roles { get; set; }

    }
}
