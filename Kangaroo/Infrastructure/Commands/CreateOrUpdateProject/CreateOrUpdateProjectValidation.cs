using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Kangaroo.Infrastructure.CommandProcessor;
using Kangaroo.Models;
using Kangaroo.Utils;

namespace Kangaroo.Infrastructure.Commands
{
    public class CreateOrUpdateProjectValidation : AbstractValidator<CreateOrUpdateProject>
    {
        private readonly ISession session;
        public CreateOrUpdateProjectValidation(ISession session)
        {
            this.session = session;
            ConfigureValidation();
        }

        private void ConfigureValidation()
        {
            RuleFor(p => p.Project)
                .NotNull();
            RuleFor(p => p.Project.Name)
                .NotEmpty();
            RuleFor(p => p.Project.Name)
                .Must(BeAUniqueProjectName)
                .WithMessage("The project must have a unique name");
        }
        private bool BeAUniqueProjectName(string projectName)
        {
            return session.GetQueryable<Project>()
                        .Where(p => p.Name == projectName).None();

        }
    }
}