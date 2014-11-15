using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kangaroo.Infrastructure.CommandProcessor
{
    public class CommandHandlerNotFoundException : Exception
    {
        public CommandHandlerNotFoundException(Type type)
            : base(string.Format("Command handler not found for command type: {0}", type))
        {
        }
    }
}
