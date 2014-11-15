using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kangaroo.Infrastructure.CommandProcessor;

namespace Kangaroo.Infrastructure
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        void Execute(TCommand command);
    }
}