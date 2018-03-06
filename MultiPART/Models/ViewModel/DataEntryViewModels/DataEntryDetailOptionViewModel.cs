using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MultiPART.Services;

namespace MultiPART.Models.ViewModel.DataEntryViewModels
{
    public class DataEntryDetailOptionViewModel: DataEntryDetailViewModel
    {
        [Display(Name = "Options")]
        public int OptionID { get; set; }

        public IEnumerable<SelectListItem> Options { get; set; }
        public override bool Validate(IValidationDictionary validationDictionary)
        {
            if (OptionID == 0)
            {
                validationDictionary.AddError("Properties", "You must select an option");
                return false;
            }
            return true;
        }
    }
}