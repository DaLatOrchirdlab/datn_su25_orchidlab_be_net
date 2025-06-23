using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.LabRoom.CreateLabRoom
{
    public class CreateLabRoomCommandValidator : AbstractValidator<CreateLabRoomCommand>
    {
        public CreateLabRoomCommandValidator() 
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
            RuleFor(x => x.Description.Count())
                .LessThanOrEqualTo(200)
                .WithMessage("Description is too long.");
        }
    }
}
