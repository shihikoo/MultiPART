using MultiPART.Models.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MultiPART.Models.LookupTable
{
    public class Question
    {
        public Question()
        {
            BehavioralScores = new HashSet<BehavioralScore>();
            Status = "Current";
        }

        public int QuestionID { get; set; }

        public string SectionNumber { get; set; }

        public string Content { get; set; }

        public string Instruction { get; set; }

        [ForeignKey("Checklist")] 
        public int ChecklistID { get; set; }

        //--------------------//
        public string Status { get; set; }

        //--------------------//
        /*virtual*/

        public virtual ProcedureDetail Checklist { get; set; }

        public ICollection<BehavioralScore> BehavioralScores { get; set; }

    }
}