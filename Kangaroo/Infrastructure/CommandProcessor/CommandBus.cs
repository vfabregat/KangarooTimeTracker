using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Results;
using Kangaroo.Utils;

namespace Kangaroo.Infrastructure.CommandProcessor
{
    public class CommandBus : ICommandBus
    {
        public ICommandResult Submit<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = DependencyResolver.Current.GetService<ICommandHandler<TCommand>>();
            if (!((handler != null) && handler is ICommandHandler<TCommand>))
            {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }
            var validationResult = this.Validate<TCommand>(command);
            if (validationResult.IsValid)
                handler.Execute(command);
            return new CommandResult(validationResult);
        }
        public ValidationResult Validate<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = DependencyResolver.Current.GetService<AbstractValidator<TCommand>>();
            if (handler != null && handler is IValidator<TCommand>)
            {
                return handler.Validate(command);
            }
            return new ValidationResult();
        }
    }
}