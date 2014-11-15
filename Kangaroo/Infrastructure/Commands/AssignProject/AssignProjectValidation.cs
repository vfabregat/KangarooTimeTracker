using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Kangaroo.Infrastructure.CommandProcessor;

namespace Kangaroo.Infrastructure.Commands
{
    public class AssignProjectValidation : AbstractValidator<AssignProject>
    {
        public AssignProjectValidation()
        {
            RuleFor(c => c.ProjectName).NotEmpty().NotNull();
            RuleFor(c => c.UserName).NotEmpty().NotNull();
        }
    }
}