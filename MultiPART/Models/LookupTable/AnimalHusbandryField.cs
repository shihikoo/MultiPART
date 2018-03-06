using MultiPART.Models.Table;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPART.Models.LookupTable
{
    public class AnimalHusbandryField : ISoftDeletable
    {

        public AnimalHusbandryField()
        {
            AnimalHusbandries = new HashSet<AnimalHusbandry>();
            AnimalHusbandryOptions = new HashSet<AnimalHusbandryOption>();

            Status = "Current";
        }

        public int AnimalHusbandryFieldID { get; set; }

        public string AnimalHusbandryFieldName { get; set; }

        [ForeignKey("Options")]
        public int AnimalHusbandryFieldTypeID { get; set; }

        public string Status { get; set; }

        //virtual
        public virtual Option Options { get; set; }

        public virtual ICollection<AnimalHusbandry> AnimalHusbandries { get; set; }

        public virtual ICollection<AnimalHusbandryOption> AnimalHusbandryOptions { get; set; }

    }
}