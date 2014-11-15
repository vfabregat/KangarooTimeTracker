using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kangaroo.Models;

namespace Kangaroo.Infrastructure.Commands
{
    public class CreateOrUpdateProject : ICommand
    {
        public Project Project { get; private set; }

        public CreateOrUpdateProject(Project project)
        {
            this.Project = project;
        }
    }
}