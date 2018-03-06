using System.Web;

namespace MultiPART.Utilities
{
    public static class MyUtils
    {
        public static bool HasFile(this HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0) ? true : false;
        }
    }
}