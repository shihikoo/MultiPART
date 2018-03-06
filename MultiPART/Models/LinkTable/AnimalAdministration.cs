using MultiPART.Models.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MultiPART.Models.LinkTable
{
    public class AnimalAdministration : ISoftDeletable
    {
        public AnimalAdministration()
        {
            Status = "Current";
            CreatedOn = DateTimeOffset.Now;
            DataEntries = new HashSet<DataEntry>();
            LastUpdatedOn = DateTimeOffset.Now;
        }
        
        [Key]
        public int AnimalAdministrationID { get; set; }
        
        public int AnimalID { get; set; }
        public int AdministrationID { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string Status { get; set; }
        public int CreatedBy { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public int LastUpdatedBy { get; set; }

        private DateTimeOffset? _lastUpdatedOn;

        [Required]
        public DateTimeOffset LastUpdatedOn
        {
            get { return _lastUpdatedOn ?? DateTimeOffset.Now; }
            set { _lastUpdatedOn = value; }
        }
        [ForeignKey("AnimalID")]
        public virtual Animal Animal { get; set; }

        [ForeignKey("AdministrationID")]
        public virtual Administration Administration { get; set; }

        public virtual ICollection<DataEntry> DataEntries { get; set; }

        [NotMapped]
        public bool IsActive
        {
            get { return (Status != "Deleted"); }
        }

        public void SoftDelete(bool cascade)
        {
            Status = "Deleted";
            if (!cascade) return;
            foreach (var dataEntry in DataEntries)
            {
                dataEntry.SoftDelete(true);
            }
        }
    }
}