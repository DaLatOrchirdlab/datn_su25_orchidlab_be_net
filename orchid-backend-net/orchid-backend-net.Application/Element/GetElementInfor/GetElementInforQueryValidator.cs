using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Element.GetElementInfor
{
    public class GetElementInforQueryValidator : AbstractValidator<GetElementInforQuery>
    {
        public GetElementInforQueryValidator() 
        {
            RuleFor(x => x.ID)
                .NotEmpty()
                .NotNull()
                .WithMessage("Element ID can not null.");

        }
    }
}
