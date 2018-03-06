using MultiPART.Models.LinkTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MultiPART.Models.Table
{
    public class Administration : ISoftDeletable
    {
        public Administration()
        {
            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;
            LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;

            Status = "Current";
        }

        public int AdministrationID { get; set; }

        public int ProcedureID { get; set; }

        public int StartTime { get; set; }

        public int EndTime { get; set; }

        public string Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public int LastUpdatedBy { get; set; }

        private DateTimeOffset? lastUpdatedOn;
        [Required]
        public DateTimeOffset LastUpdatedOn
        {
            get { return lastUpdatedOn ?? DateTimeOffset.Now; }
            set { lastUpdatedOn = value; }
        }

        public virtual Procedure Procedure { get; set; }

        public virtual ICollection<AnimalAdministration> AnimalAdministrations { get; set; }

    }
}