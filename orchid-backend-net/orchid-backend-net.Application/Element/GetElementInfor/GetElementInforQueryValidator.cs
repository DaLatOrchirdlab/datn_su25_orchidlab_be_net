using FluentValidation;

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
