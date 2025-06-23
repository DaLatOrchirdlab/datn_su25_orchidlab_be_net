using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.LabRoom.GetLabRoomInfor
{
    public class GetLabRoomInforQueryValidator : AbstractValidator<GetLabRoomInforQuery>
    {
        public GetLabRoomInforQueryValidator() 
        {
            Configuration();
        }
        void Configuration()
        {
            RuleFor(x => x.ID)
                .NotEmpty()
                .NotNull()
                .WithMessage("Id can not null.");
        }
    }
}
