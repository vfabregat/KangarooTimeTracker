using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kangaroo.Infrastructure.Queries.Reports;
using Kangaroo.Models;

namespace Kangaroo.Infrastructure.Queries
{
    public class DashboardQuery : IQuery<DashboardModel>
    {
        private readonly ProjectsHoursReport projectsHoursQuery;
        private readonly ProjectsHoursByUserReport projectsHoursByUserReport;
        public DashboardQuery(ProjectsHoursReport projectsHoursQuery,
            ProjectsHoursByUserReport projectsHoursByUserReport)
        {
            this.projectsHoursQuery = projectsHoursQuery;
            this.projectsHoursByUserReport = projectsHoursByUserReport;
        }
        public DashboardModel Run()
        {
            var model = new DashboardModel();
            DateTime today = DateTime.Today;
            var daysInCurrentMonth = DateTime.DaysInMonth(today.Year, today.Month);
            DateTime endOfMonth = new DateTime(today.Year, today.Month, daysInCurrentMonth);
            DateTime yearStart = new DateTime(today.Year, 1, 1);
            DateTime yearEnd = new DateTime(today.Year, 12, 31);
            model.ProjectsAndHours = projectsHoursQuery.Run(endOfMonth.AddDays(-daysInCurrentMonth), endOfMonth);
            model.ProjectsAndHoursByUser = projectsHoursByUserReport.Run();

            return model;
        }
    }
}