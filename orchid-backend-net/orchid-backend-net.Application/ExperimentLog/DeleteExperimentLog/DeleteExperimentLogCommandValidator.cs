using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.ExperimentLog.DeleteExperimentLog
{
    public class DeleteExperimentLogCommandValidator : AbstractValidator<DeleteExperimentLogCommand>
    {
        public DeleteExperimentLogCommandValidator() 
        {
            Configuration();
        }
        void Configuration()
        {
            RuleFor(x => x.ID)
                .NotEmpty()
                .NotNull()
                .WithMessage("ID can not be null.");
        }
    }
}
