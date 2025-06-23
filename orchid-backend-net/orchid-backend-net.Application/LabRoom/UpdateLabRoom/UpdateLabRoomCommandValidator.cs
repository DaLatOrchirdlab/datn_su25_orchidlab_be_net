using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.LabRoom.UpdateLabRoom
{
    public class UpdateLabRoomCommandValidator : AbstractValidator<UpdateLabRoomCommand>
    {
        public UpdateLabRoomCommandValidator() 
        {
            Configuration();
        }
        void Configuration()
        {
            RuleFor(x => x.Description.Count())
                .LessThanOrEqualTo(200)
                .WithMessage("Description is too long.");
        }
    }
}
