using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kangaroo.Utils;

namespace Kangaroo.Models
{
    public class TimeSheet
    {
        public Guid Id { get; set; }
        public List<TimeEntry> TimeEntries { get; set; }

        public List<string> AssignedProjects { get; set; }

        public string SelectedProject { get; set; }
        public List<Project> AvailableProjects { get; set; }


        public List<DateTime> Week { get; private set; }

        public bool IncludeWeekends { get; set; }
        public TimeSheet(DateTime date)
        {
            IncludeWeekends = false;
            Id = Guid.NewGuid();
            InitializeWeek(date);
            AssignedProjects = new List<string>();
            AvailableProjects = new List<Project>();
            TimeEntries = new List<TimeEntry>();
        }

        public TimeSheet()
            : this(DateTime.Now)
        {

        }

        private void InitializeWeek(DateTime date)
        {
            Week = new List<DateTime>();

            var dt = date.StartOfWeek(DayOfWeek.Monday);
            var totalDays = IncludeWeekends ? 7 : 5;

            for (int i = 0; i < totalDays; i++)
            {
                Week.Add(dt.AddDays(i));
            }
        }
    }
}