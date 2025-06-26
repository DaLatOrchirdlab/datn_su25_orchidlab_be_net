using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Report.CreateReport
{
    public class CreateReportCommandValidator : AbstractValidator<CreateReportCommand>
    {
        public CreateReportCommandValidator() 
        {
            Configuration();
        }
        void Configuration()
        {
            RuleFor(x => x.Description)
                .MaximumLength(300)
                .MinimumLength(50)
                .NotEmpty()
                .NotNull()
                .WithMessage("in valid Description.");
            RuleFor(x => x.Name)
                .MaximumLength(50)
                .NotNull()
                .NotEmpty()
                .WithMessage("in valid name.");
            RuleFor(x => x.Technician)
                .NotEmpty()
                .NotNull()
                .WithMessage("Not found teachnician.");
            RuleFor(x => x.Sample)
                .NotNull()
                .NotEmpty()
                .WithMessage("Not found Sample.");
        }
    }
}
