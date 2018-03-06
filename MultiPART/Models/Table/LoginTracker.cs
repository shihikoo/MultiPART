using System;

namespace MultiPART.Models.Table
{
    public class LoginTracker
    {
        public int LoginTrackerID { get; set; }

        public int LoginIP { get; set; }

        public DateTimeOffset LoginTime { get; set; }

        public string LoginName { get; set; }

        public string LoginStatus { get; set; }
    }
}