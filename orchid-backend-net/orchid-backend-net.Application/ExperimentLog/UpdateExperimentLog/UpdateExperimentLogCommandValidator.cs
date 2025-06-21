using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.ExperimentLog.UpdateExperimentLog
{
    public class UpdateExperimentLogCommandValidator : AbstractValidator<UpdateExperimentLogCommand>
    {
        public UpdateExperimentLogCommandValidator() 
        {
            Configuration();
        }
        void Configuration()
        {
            RuleFor(x => x.Description.Count())
                .LessThanOrEqualTo(200)
                .WithMessage("Description is too long.");
            RuleFor(x => x.Hybridization.Count())
                .LessThanOrEqualTo(2)
                .GreaterThanOrEqualTo(1)
                .WithMessage("chosing parents error.");
        }
    }
}
