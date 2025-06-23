using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.ExperimentLog.GetExperimentLogInfor
{
    public class GetExperimentLogInforQueryValidator : AbstractValidator<GetExperimentLogInforQuery>
    {
        public GetExperimentLogInforQueryValidator() 
        {
            Configuration();
        }
        public void Configuration()
        {
            RuleFor(x => x.ID)
                .NotEmpty()
                .NotNull()
                .WithMessage("ID can not empty.");
        }
    }
}
