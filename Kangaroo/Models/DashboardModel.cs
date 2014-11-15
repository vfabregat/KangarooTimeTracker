using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.Highcharts;

namespace Kangaroo.Models
{
    public class DashboardModel
    {
        public IEnumerable<ProjectHoursModel> ProjectsAndHours { get; set; }
        public IEnumerable<ProjectHoursByUserModel> ProjectsAndHoursByUser { get; set; }

        public Highcharts ChartData { get; set; }
    }
}
