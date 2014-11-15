using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation.Results;

namespace Kangaroo.Infrastructure.CommandProcessor
{
    public class CommandResult : ICommandResult
    {
        public ValidationResult ValidationResult { get; private set; }
        public CommandResult(ValidationResult result)
        {
            this.Success = result.IsValid;
            this.ValidationResult = result;
        }
        public CommandResult(bool success)
        {
            this.Success = success;
        }

        public bool Success { get; protected set; }
    }
}