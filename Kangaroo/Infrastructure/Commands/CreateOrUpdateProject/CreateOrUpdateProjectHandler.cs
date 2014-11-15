using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kangaroo.Models;

namespace Kangaroo.Infrastructure.Commands
{
    public class CreateOrUpdateProjectHandler : ICommandHandler<CreateOrUpdateProject>
    {
        private readonly ISession session;
        public CreateOrUpdateProjectHandler(ISession session)
        {
            this.session = session;
        }
        public void Execute(CreateOrUpdateProject command)
        {
            session.Add(command.Project);
        }
    }
}