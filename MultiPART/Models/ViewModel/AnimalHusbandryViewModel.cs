using System.Web.Mvc;

namespace MultiPART.Models.ViewModel
{
    public class AnimalHusbandryViewModel
    {     
        public int AnimalHusbandryID { get; set; }

        public int MultiPARTProjectID { get; set; }

        public int ResearchgroupID { get; set; }

        public int StrainID { get; set; }

        public int FieldID { get; set; }

        public string FieldType { get; set; }

        public string FieldName { get; set; }

        public int? OptionID { get; set; }

        public string Value { get; set; }

        public SelectList Options { get; set; }

        //[DisplayName("Animal Husbandry Label")]
        //public string AnimalHusbandryLabel { get; set; }
        //[DisplayName("Bedding Material")]
        //public int BeddingMaterialID { get; set; }
        //[DisplayName("Handling")]
        //public int HandlingID { get; set; }
        //[DisplayName("Biosecurity Level")]
        //public int BiosecurityLevelID { get; set; }
        //[DisplayName("Ventilation")]
        //public int VentilationID { get; set; }
        //[DisplayName("Water Access")]
        //public int WaterAccessID { get; set; }
        //[DisplayName("Cage Height")]
        //public float CageSizeHeight { get; set; }
        //[DisplayName("Cage Width")]
        //public float CageSizeWidth { get; set; }
        //[DisplayName("Cage Depth")]
        //public float CageSizeDepth { get; set; }
        //[DisplayName("Cage Change / Week")]
        //public int NumberOfCageChangePerWeek { get; set; }
        //[DisplayName("Enrichment Type")]
        //public string EnrichmentType { get; set; }
        //[DisplayName("Food Type")]
        //public string FoodType { get; set; }
        //[DisplayName("Food Deprivation")]
        //public string FoodDeprivation { get; set; }
        //[DisplayName("Food Deprivation Schedule")]
        //public string FoodDeprivationSchedule { get; set; }
        //[DisplayName("Lighting Hours Light / 24hours")]
        //public int LightingHoursLightPer24hours { get; set; }
        //[DisplayName("Number Of Experimenters Interact With Animals")]
        //public int NumberOfExperimentersInteractWithAnimals { get; set; }
        //[DisplayName("Animal Housed Per Cage Prior To Surgical Procedure")]
        //public int AnimalHousedPerCagePriorToSurgicalProcedure { get; set; }
        //[DisplayName("Animals / Cage")]
        //public int NumberOfAnimalsPerCage { get; set; }
        //[DisplayName("Minimum Room Temperature")]
        //public string RoomTemperatureMin { get; set; }
        //[DisplayName("Max Room Temperature")]
        //public string RoomTemperatureMax { get; set; }
        //[DisplayName("Minimum Relative Humidity")]
        //public string RelativeHumidityMin { get; set; }
        //[DisplayName("Maximum Relative Humidity")]
        //public string RelativeHumidityMax { get; set; }

    }
}