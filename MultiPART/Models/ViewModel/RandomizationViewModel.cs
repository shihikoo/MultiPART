namespace MultiPART.Models.ViewModel
{
    public class RandomizationViewModel
    {
        //public int AnimalID { get; set; }

        //public int AnimalLabel { get; set; }

        public int CohortID { get; set; }

        public string CohortLabel { get; set; }

        public int TotalNumberOfAnimals { get; set; }

        public int CurrentNumberOfAnimals { get; set; }

        public int RemainNumberOfAnimals
        {
            get { return TotalNumberOfAnimals - CurrentNumberOfAnimals; }
        }

        public bool CohortFull {
            get
            {
                return (TotalNumberOfAnimals == CurrentNumberOfAnimals);
            } 
        }

        public int probability { get; set; }

    }
}