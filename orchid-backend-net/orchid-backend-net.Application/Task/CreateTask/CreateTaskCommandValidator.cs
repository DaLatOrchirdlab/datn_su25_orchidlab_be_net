using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Task.CreateTask
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator() 
        {
            Configuration();
        }

        void Configuration()
        {
            RuleFor(x => x.End_date)
                .GreaterThan(x => x.Start_date)
                .WithMessage("Task can not end before start time.");
            RuleFor(x => x.End_date)
                .NotNull()
                .NotEmpty()
                .WithMessage("End time can not be empty.");
            RuleFor(x => x.Start_date)
                .GreaterThanOrEqualTo(x => x.Create_at)
                .WithMessage("Task can not end before start time.");
            RuleFor(x => x.Start_date)
                .NotNull()
                .NotEmpty()
                .WithMessage("End time can not be empty.");

        }
    }
}
