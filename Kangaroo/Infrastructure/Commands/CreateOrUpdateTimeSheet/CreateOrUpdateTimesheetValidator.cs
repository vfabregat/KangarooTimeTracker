using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace Kangaroo.Infrastructure.Commands
{
    public class CreateOrUpdateTimeSheetValidator : AbstractValidator<CreateOrUpdateTimeSheet>
    {
        public CreateOrUpdateTimeSheetValidator()
        {
            RuleFor(ts => ts.TimeEntries.Count)
                .GreaterThan(0);
                
        }
    }
}