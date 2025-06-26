using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Method.CreateMethod
{
    public class CreateMethodCommandValidator : AbstractValidator<CreateMethodCommand>
    {
        public CreateMethodCommandValidator() 
        {
            Configuration();
        }
        void Configuration()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name can not be null.");
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Description can not be null.");
            RuleFor(x => x.Name.Count())
                .LessThanOrEqualTo(50)
                .WithMessage("Name is too long");
            RuleFor(x => x.Description.Count())
                .LessThanOrEqualTo(500)
                .WithMessage("Description is too long.");
        }
    }
}
