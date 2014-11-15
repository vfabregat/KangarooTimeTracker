using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Results;

namespace Kangaroo.Infrastructure.CommandProcessor
{
    public interface ICommandResult
    {
        ValidationResult ValidationResult { get; }
        bool Success { get; }
    }
}
