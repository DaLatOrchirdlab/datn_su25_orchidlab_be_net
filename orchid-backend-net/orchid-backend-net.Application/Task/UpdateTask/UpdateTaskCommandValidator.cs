using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Task.UpdateTask
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator() 
        {
            Configuration();
        }

        void Configuration()
        {
            RuleFor(x => x.ID)
                .NotEmpty()
                .NotNull()
                .WithMessage("ID can not null");
            RuleFor(x => x.Status)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Your status is out of scope.");
            RuleFor(x => x.Status)
                .LessThanOrEqualTo(5)
                .WithMessage("Your status is out of scope.");
        }
    }
}
