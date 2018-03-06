using System.Web.Mvc;

namespace MultiPART.Models.ViewModel
{
    public class LookupTableListViewModel
    {

        public string Lookuptablename { get; set; }

        public SelectList Lookuptables { get; set; }

        public string Categoryname { get; set; }

        public LookupTableViewModel LookupTableVM { get; set; }

    }
}