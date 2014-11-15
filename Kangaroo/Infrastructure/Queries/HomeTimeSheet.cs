using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kangaroo.Models;
using MongoDB.Driver.Linq;

namespace Kangaroo.Infrastructure.Queries
{
    public class HomeTimeSheet : IQuery<DateTime?, TimeSheet>
    {
        private readonly ISession session;
        public HomeTimeSheet(ISession session)
        {
            this.session = session;
        }

        public TimeSheet Run(DateTime? date)
        {
            if (!date.HasValue)
                date = DateTime.Now;
            var timeSheet = new TimeSheet(date.Value);
            var startDate = timeSheet.Week.First();
            var endDate = timeSheet.Week.Last();

            timeSheet.TimeEntries = session.GetQueryable<TimeEntry>()
                .Where(t => startDate <= t.Date &&
                    endDate >= t.Date
                    && t.UserName == MvcApplication.GetCurrentUser).ToList();

            timeSheet.AssignedProjects = session.GetQueryable<ProjectAssignation>().Select(p => p.ProjectName).ToList();

            timeSheet.AvailableProjects = session.GetQueryable<Project>().Where(p=> !timeSheet.AssignedProjects.Contains(p.Name)).ToList();

            return timeSheet;
        }
    }
}