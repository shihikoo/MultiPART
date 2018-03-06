using System.Collections.Generic;
using System.Dynamic;

namespace MultiPART
{
    public enum EntryFieldType
    {
        Text,
        Lookup,
        DateTime,
        Double,
        Integer,
        Date,
        File,
        Time
    }

    public enum DataEntryViewModelType
    {
        File,
        Option,
        Value
    }

    public enum StatusType
    { 
        Current,
        Completed,
        Deleted
    }

    public static class ProcedurePurpose
    {
        public static Dictionary<string, string> ProcedureDictionary = new Dictionary<string, string>
        {
            {"Comorbidity Induction","comorbidityInduction"},
            {"Disease Model Induction", "diseaseModelInduction"},
            {"Treatment", "treatment"},
            {"Post-Operative Analgesia", "postOperativeAnalgesia"},
            {"Outcome Assessment", "outcomeAssessment"},
            {"Anaesthesia", "anaesthesia"},
            {"Mortality Report", "mortalityReport"}
        };
    }


    public enum AssessType
    {
        Edit,
        View,
        None
    }

}