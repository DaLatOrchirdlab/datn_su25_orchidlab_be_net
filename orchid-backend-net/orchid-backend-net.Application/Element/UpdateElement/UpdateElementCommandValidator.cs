using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Element.UpdateElement
{
    public class UpdateElementCommandValidator : AbstractValidator<UpdateElementCommand>
    {
        public UpdateElementCommandValidator() 
        {
            Configuration();
        }
        void Configuration()
        {
            RuleFor(x => x.Description.Count())
                .LessThanOrEqualTo(250)
                .WithMessage("Element description is too long.");
        }
    }
}
