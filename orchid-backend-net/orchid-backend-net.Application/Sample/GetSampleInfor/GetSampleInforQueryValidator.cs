using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Sample.GetSampleInfor
{
    public class GetSampleInforQueryValidator : AbstractValidator<GetSampleInforQuery>
    {
        public GetSampleInforQueryValidator() 
        {
            Configuration();
        }
        void Configuration()
        {
            RuleFor(x => x.ID)
                .NotEmpty()
                .NotNull()
                .WithMessage("Sample ID can not empty.");
        }
    }
}
