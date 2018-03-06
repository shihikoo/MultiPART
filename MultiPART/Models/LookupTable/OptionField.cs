using System.Collections.Generic;

namespace MultiPART.Models.LookupTable
{
    public class OptionField : ISoftDeletable
    {

        public OptionField()
            {
                Options = new HashSet<Option>();

                Status = "Current";
            }

        public int OptionFieldID { get; set; }

        public string OptionFieldName { get; set; }

        public string TableName { get; set; }

        public string Status { get; set; }


        public virtual ICollection<Option> Options { get; set; }

    }
}