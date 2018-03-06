using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MultiPART.Models.ViewModel
{
    public class LookupTableViewModel
    {

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Status { get; set; }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public SelectList Categories { get; set; }

        public int PurposeOrTypeID { get; set; }

        public SelectList PurposesTypes { get; set; }

        public string LookupTableName { get; set; }

        public bool Multiple { get; set; }
    }
}