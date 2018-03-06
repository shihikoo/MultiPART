using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MultiPART.Models.Table;

namespace MultiPART.Models.LinkTable
{

    public class DataEntry : ISoftDeletable
    {
        public DataEntry()
        {
            UserDataentryAssignments = new HashSet<UserDataentryAssignment>();
            DataEntryDetails = new HashSet<DataEntryDetail>().ToList();
            Status = "Current";
        }
        [Key]
        public int DataEntryID { get; set; }

        [ForeignKey("AnimalAdministration")]
        public int AnimalAdministrationID { get; set; }

        [ForeignKey("DataEntryDesign")]
        public int DataEntryDesignDataEntryDesignID { get; set; }

        /*virtual*/
        public virtual AnimalAdministration AnimalAdministration { get; set; }

        public virtual DataEntryDesign DataEntryDesign { get; set; }

        public virtual IList<DataEntryDetail> DataEntryDetails { get; set; }

        public virtual ICollection<UserDataentryAssignment> UserDataentryAssignments { get; set; }

        public string Status { get; set; }

        [NotMapped]
        public bool IsActive
        {
            get { return (Status != "Deleted"); }
        }

        public void SoftDelete(bool cascade)
        {
            Status = "Deleted";
            if (cascade)
            {
                foreach (var detail in DataEntryDetails)
                {
                    detail.SoftDelete(true);
                }
            }
        }
    }
}