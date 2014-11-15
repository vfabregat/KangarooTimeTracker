using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation.Results;

namespace Kangaroo.Infrastructure.CommandProcessor
{
    public interface ICommandBus
    {
        ICommandResult Submit<TCommand>(TCommand command) where TCommand : ICommand;
        ValidationResult Validate<TCommand>(TCommand command)
                                                where TCommand : ICommand;
    }
}