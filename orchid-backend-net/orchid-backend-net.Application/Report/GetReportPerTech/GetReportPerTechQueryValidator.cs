using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Report.GetReportPerTech
{
    public class GetReportPerTechQueryValidator : AbstractValidator<GetReportPerTechQuery>
    {
        public GetReportPerTechQueryValidator() 
        {
            
        }

        void Configuration()
        {
            RuleFor(x => x.techID)
                .NotEmpty()
                .NotNull()
                .WithMessage("Missing technicianID");
        }
    }
}
