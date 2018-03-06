using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Web.Mvc;

namespace MultiPART.Models.ViewModel.DataEntryViewModels
{
    public class DataEntryViewModel 
    {
        private string _animalLabel;


        public int ResearchGroupID { get; set; }
        public int ProjectID { get; set; }
        public int DiseaseModelInductionID { get; set; }
        public int ProcedureID { get; set; }
        public int AdministrationID { get; set; }
  //      public int AnimalProcedureID { get; set; }
        public int AnimalAdministrationID { get; set; }

        public string AnimalLabel
        {
            get
            {
                if (_animalLabel == null)
                {
                    return null;
                }
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_animalLabel);
            }
            set { _animalLabel = value; }
        }
        public int AnimalID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Strain { get; set; }
        public string Species { get; set; }
        [DisplayName("Procedure Name")]
        public string ProcedureName { get; set; }
        public Collection<DataEntryFieldViewModel> Properties { get; set; }
    }

    public class DataEntryFieldViewModel
    {
        public int ResearchGroupID { get; set; }
        public int ProjectID { get; set; }
        public int DiseaseModelInductionID { get; set; }
        public int ProcedureID { get; set; }
        public int AnimalID { get; set; }

        public int DataEntryID { get; set; }
        public int DesignID { get; set; }
        [DisplayName("Property Name")]
        public string DataEntryFieldName { get; set; }

        public bool Mandatory { get; set; }
        public bool Multiple { get; set; }
        public DataEntryViewModelType ViewModelType { get; set; }
        public IEnumerable<SelectListItem> Options { get; set; }
        public IList<DataEntryDetailViewModel> DataEntryDetails { get; set; }
        public bool Active { get; set; }

    }
}