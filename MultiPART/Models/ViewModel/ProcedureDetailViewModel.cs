using System.Collections.Generic;
using System.Web.Mvc;

namespace MultiPART.Models.ViewModel
{
    public class ProcedureDetailViewModel
    {
        public int ProcedureID { get; set; }

        public int FieldID { get; set; }

        public string FieldType { get; set; }

        public string FieldName { get; set; }

        public bool Multiple { get; set; }

   //     public int? ProcedureDetailID { get; set; }

        public int? OptionID { get; set; }

        public string DisplayValue { get; set; }

        public int?[] OptionIDs { get; set; }

        public string Value { get; set; }

        public IEnumerable<SelectListItem> Options { get; set; }

    }
}