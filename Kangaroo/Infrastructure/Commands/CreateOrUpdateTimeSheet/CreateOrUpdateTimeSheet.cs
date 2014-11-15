using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kangaroo.Models;

namespace Kangaroo.Infrastructure.Commands
{
    public class CreateOrUpdateTimeSheet : ICommand
    {
        public List<TimeEntry> TimeEntries{get;private set;}
        public string UserName {get; private set;}
        public CreateOrUpdateTimeSheet(List<TimeEntry> timeEntries, string userName)
        {
            this.TimeEntries = timeEntries;
            this.UserName = userName;
        }
    }
}