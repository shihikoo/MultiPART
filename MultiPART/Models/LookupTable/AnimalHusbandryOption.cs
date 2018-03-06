using MultiPART.Models.Table;
using System.Collections.Generic;

namespace MultiPART.Models.LookupTable
{
    public class AnimalHusbandryOption : ISoftDeletable
    {
        public AnimalHusbandryOption()
        {
            AnimalHusbandries = new HashSet<AnimalHusbandry>();

            Status = "Current";
        }

        public int AnimalHusbandryOptionID { get; set; }

        public int AnimalHusbandryFieldID { get; set; }

        public string AnimalHusbandryOptionName { get; set; }

        public string Status { get; set; }

        public virtual AnimalHusbandryField AnimalHusbandryFields { get; set; }

        public virtual ICollection<AnimalHusbandry> AnimalHusbandries { get; set; }

    }
}