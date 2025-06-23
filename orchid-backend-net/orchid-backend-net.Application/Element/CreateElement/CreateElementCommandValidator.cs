using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Element.CreateElement
{
    public class CreateElementCommandValidator : AbstractValidator<CreateElementCommand>
    {
        public CreateElementCommandValidator() 
        {
            Configuration();
        }
        void Configuration()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Element description can not be null.");
            RuleFor(x => x.Description.Count())
                .LessThanOrEqualTo(250)
                .WithMessage("Element description is too long.");
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Element name can not be null.");
        }
    }
}
