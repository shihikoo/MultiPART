using System.Web.WebPages;
using MultiPART.Services;

namespace MultiPART.Models.ViewModel.DataEntryViewModels
{
    public class DataEntryDetailValueViewModel :DataEntryDetailViewModel
    {

        public string Value { get; set; }

        public override bool Validate(IValidationDictionary validationDictionary)
        {
            if (Value.IsEmpty())
            {
                validationDictionary.AddError("Properties", "A file upload is required");
                return false;
            }
            return true;
        }
    }
}