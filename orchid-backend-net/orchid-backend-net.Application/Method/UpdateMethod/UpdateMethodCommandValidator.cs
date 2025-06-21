using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Method.UpdateMethod
{
    public class UpdateMethodCommandValidator : AbstractValidator<UpdateMethodCommand>
    {
        public UpdateMethodCommandValidator() 
        {
            Configuration();
        }
        void Configuration()
        {
            RuleFor(x => x.Name.Count())
                .LessThanOrEqualTo(50)
                .WithMessage("Name is too long");
            RuleFor(x => x.Description.Count())
                .LessThanOrEqualTo(500)
                .WithMessage("Description is too long.");
        }
    }
}
