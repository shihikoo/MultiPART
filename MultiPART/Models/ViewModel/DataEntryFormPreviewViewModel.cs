namespace MultiPART.Models.ViewModel
{
    public class DataEntryFormPreviewViewModel
    {
        public int ProcedureID { get; set; }
        public int ProjectID { get; set; }
        public string DataEntryFieldName { get; set; }

        public string Mandatory { get; set; }

        public string Multiple { get; set; }

    }
}