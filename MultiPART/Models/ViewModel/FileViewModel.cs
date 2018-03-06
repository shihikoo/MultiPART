using System.Web;

namespace MultiPART.Models.ViewModel
{
    public class FileViewModel
    {
        public HttpPostedFileBase File { get; set; }
        public string Description { get; set; }

    }
}