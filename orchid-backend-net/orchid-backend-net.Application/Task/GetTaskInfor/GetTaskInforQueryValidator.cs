using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Task.GetTaskInfor
{
    public class GetTaskInforQueryValidator : AbstractValidator<GetTaskInforQuery>
    {
        public GetTaskInforQueryValidator() 
        {
            Configuration();
        }
        void Configuration()
        {
            RuleFor(x => x.ID)
                .NotEmpty()
                .NotNull()
                .WithMessage("ID can not null.");
        }
    }
}
