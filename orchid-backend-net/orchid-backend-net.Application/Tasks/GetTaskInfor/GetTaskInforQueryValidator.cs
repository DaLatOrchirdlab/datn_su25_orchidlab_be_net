using FluentValidation;

namespace orchid_backend_net.Application.Tasks.GetTaskInfor
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
