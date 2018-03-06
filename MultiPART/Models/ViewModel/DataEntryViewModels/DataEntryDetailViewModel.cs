using System;
using System.ComponentModel;
using MultiPART.Services;

namespace MultiPART.Models.ViewModel
{
    public abstract class  DataEntryDetailViewModel
    {
        public int DataEntryDataEntryID { get; set; }

        public int DataEntryDetailID { get; set; }

        [DisplayName("Start Time")]
        public DateTimeOffset StartTime { get; set; }

        [DisplayName("End Time")]
        public DateTimeOffset EndTime { get; set; }

        public int DesignID { get; set; }
        public bool Active { get; set; }

        public abstract bool Validate(IValidationDictionary validationDictionary);


    }
}