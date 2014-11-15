using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Kangaroo.Infrastructure;
using Kangaroo.Models;
using System.Globalization;

namespace Kangaroo.Controllers
{
    public class ChartData
    {
        public decimal Quantity { get; set; }
        public int Month { get; set; }
        public string Name { get; set; }
    }
    public class TimeEntryController : ApiController
    {
        private readonly ISession session;
        public TimeEntryController(ISession session)
        {
            this.session = session;
        }

        public IEnumerable<ChartData> Get()
        {
            DateTime today = DateTime.Today;
            DateTime yearStart = new DateTime(today.Year, 1, 1);
            DateTime yearEnd = new DateTime(today.Year, 12, 31);

            var entries = session.GetQueryable<TimeEntry>()
                .Where(t => t.Date <= yearEnd
                    && t.Date >= yearStart).ToList();

            var projects = entries.Select(e => e.Project).Distinct();




            var groupedEntries = GetAllMonths(projects).GroupJoin(
                entries,
                m => m.Key,
                e => e.Date.Month,
                (m, e) => e
                    .Select(entry => new ChartData { Name = entry.Project, Month = entry.Date.Month, Quantity = entry.Quantity.Value })
                    .DefaultIfEmpty(new ChartData { Name = m.Value, Month = m.Key, Quantity = 0 }))
                    .SelectMany(e => e)
                    .GroupBy(s => new { s.Month, s.Name })
                    .Select(c =>
                        new ChartData
                        {
                            Month = c.Key.Month,
                            Name = c.Key.Name,
                            Quantity = c.Sum(s => s.Quantity)
                        });

            return groupedEntries.ToList().OrderBy(t => t.Name).ThenBy(t => t.Month);
        }

        private IEnumerable<KeyValuePair<int, string>> GetAllMonths(IEnumerable<string> projects)
        {
            foreach (var project in projects)
            {
                for (int i = 1; i <= 12; i++)
                {
                    yield return new KeyValuePair<int, string>(i, project);
                }
            }
        }
    }
}
