using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace MultiPART.Models.Table
{
    public class File : ISoftDeletable
    {
        public File()
        {
            DataEntryDetailFiles = new HashSet<DataEntryDetailFile>();
            MultiPARTProjects = new HashSet<MultiPARTProject>();
            //DataEntryDetailFiles = new HashSet<DataEntryDetailFile>();
            Status = "Current";

            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;
        }

        [Key]
        public int FileID { get; set; }

        public string FileName { get; set; }

        public string FileType { get; set; }

        public string FileExtension { get; set; }

        [DataType(DataType.Url)]
        public string FileUrl { get; set; }

        public string Description { get; set; }
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

        public virtual ICollection<MultiPARTProject> MultiPARTProjects { get; set; }

        public virtual ICollection<DataEntryDetailFile> DataEntryDetailFiles { get; set; }

        //public virtual Publication Publications { get; set; }

        

    }
}