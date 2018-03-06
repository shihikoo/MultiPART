using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MultiPART.Models.ViewModel;

namespace MultiPART.Services
{
    public static class RandomisationService
    {
        
        public static int RandomizeAnimal(List<RandomizationViewModel> cohorts)
        {
            var random = new Random();
            var rtotal = cohorts.Select(c => c.RemainNumberOfAnimals).Sum();
            if (rtotal == 0) throw new Exception("Allocation Already Complete");
            var cohortid = 0;

            var rand1 = random.Next(0,100);

            var min = 0;
            var max = 0;
            foreach (var cohort in cohorts)
            {
                cohort.probability = Convert.ToInt32((cohort.RemainNumberOfAnimals * 1.0) / rtotal * 100);
                max = max + cohort.probability;
                if (rand1 >= min && rand1 < max)
                {
                    cohortid = cohort.CohortID;
                    break;
                }
                min = max;
            }

            const int nudgefactor = 90;

            var rand2 = random.Next(0, 100);
            var lastid = cohortid;
            if (rand2 < nudgefactor) { return cohortid; }
            else
            {
                var newcohorts = from c in cohorts
                                 where c.CohortID != cohortid
                                 select c;

                rtotal = newcohorts.Select(c => c.RemainNumberOfAnimals).Sum();
                if (rtotal == 0) return lastid;

                var rand3 = random.Next(0, 100);

                min = 0;
                max = 0;
                foreach (var cohort in newcohorts)
                {
                    cohort.probability = Convert.ToInt32(cohort.RemainNumberOfAnimals * 1.0 / rtotal * 100);
                    max = max + cohort.probability;
                    if (rand3 >= min && rand3 < max) cohortid = cohort.CohortID;
                    min = max;
                    lastid = cohortid;
                }
                if (cohortid == 0) cohortid = lastid;

            }

            return cohortid;

        }

        //public static class RandomGen2(int low=0, int high=100)
        //{
        //    private static Random _global = new Random();
        //    [ThreadStatic]
        //    private static Random _local;

        //    public static int Next()
        //    {
        //        Random inst = _local;
        //        if (inst == null)
        //        {
        //            int seed;
        //            lock (_global) seed = _global.Next();
        //            _local = inst = new Random(seed);
        //        }
        //        return inst.Next(low, high);
        //    }
        //}
    }
}