using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices;
using System.Net;
using System.Web;
using MultiPART.Models.Table;
using MultiPART.Services;
using MultiPART.Utilities;

namespace MultiPART.Models.ViewModel.DataEntryViewModels
{
    public class DataEntryDetailFileViewModel : DataEntryDetailViewModel, IFileWrapper
    {
        public int ProjectID { get; set; }
        public int ResearchGroupID { get; set; }

        public int AnimalID { get; set; }

        public int ProcedureID { get; set; }

        public int FileID { get; set; }

        [DisplayName("File Type")]
        public string FileType { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File { get; set; }

        [DataType(DataType.Url)]
        public string FileUrl { get; set; }

        public string Description { get; set; }

        public string RelativePath
        {
            get
            {
                return "Project-" + ProjectID + "/ResearchGroup-"
                    + ResearchGroupID + "/Animal-" + AnimalID + "/Procedure-" + ProcedureID + "/";
            }
        }


        public override bool Validate(IValidationDictionary validationDictionary)
        {
            if (!File.HasFile())
            {
                validationDictionary.AddError("Properties", "A file upload is required");
                return false;
            }
            return true;
        }
    }
}