using FluentValidation;

namespace orchid_backend_net.Application.ExperimentLog.CreateExperimentLog
{
    public class CreateExperimentLogCommandValidator : AbstractValidator<CreateExperimentLogCommand>
    {
        public CreateExperimentLogCommandValidator()
        {
            Configuration();
        }
        void Configuration()
        {
            RuleFor(x => x.TissueCultureBatchID)
                .NotEmpty()
                .NotNull()
                .WithMessage("Tissue culture batch can not null.");
            RuleFor(x => x.MethodID)
                .NotEmpty()
                .NotNull()
                .WithMessage("Method can not null.");
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Description can not null.");
            RuleFor(x => x.Description.Count())
                .LessThanOrEqualTo(200)
                .WithMessage("Description is too long.");
            RuleFor(x => x.Hybridization)
                .NotEmpty()
                .NotNull()
                .WithMessage("Chose parent");
            RuleFor(x => x.Hybridization.Count())
                .LessThanOrEqualTo(2)
                .GreaterThanOrEqualTo(1)
                .WithMessage("chosing parents error.");
        }
    }
}
