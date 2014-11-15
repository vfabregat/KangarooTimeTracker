using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kangaroo.Models;

namespace Kangaroo.Infrastructure.Commands
{
    public class AssignProjectHandler : ICommandHandler<AssignProject>
    {
        ISession session;
        public AssignProjectHandler(ISession session)
        {
            this.session = session;
        }
        public void Execute(AssignProject command)
        {

            session.Add<ProjectAssignation>(new ProjectAssignation()
            {
                UserName = command.UserName,
                ProjectName = command.ProjectName
            });
        }
    }
}