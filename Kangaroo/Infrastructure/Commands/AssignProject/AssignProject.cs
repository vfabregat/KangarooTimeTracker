using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kangaroo.Infrastructure.Commands
{
    public class AssignProject : ICommand
    {
        public string UserName { get; private set; }
        public string ProjectName { get; private set; }

        public AssignProject(string userName, string projectName)
        {
            this.UserName = userName;
            this.ProjectName = projectName;
        }
    }
}