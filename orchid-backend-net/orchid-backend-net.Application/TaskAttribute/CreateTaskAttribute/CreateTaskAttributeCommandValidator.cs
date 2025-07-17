using FluentValidation;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.TaskAttribute.CreateTaskAttribute
{
    public class CreateTaskAttributeCommandValidator : AbstractValidator<CreateTaskAttributeCommand>
    {
        private readonly ITaskAttributeRepository _taskAttributeRepository;
        public CreateTaskAttributeCommandValidator(ITaskAttributeRepository taskAttributeRepository)
        {
            _taskAttributeRepository = taskAttributeRepository;
            Configure();
        }

        private void Configure()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name cannot be null or empty.");
            RuleFor(x => x.Name)
                .MustAsync(async (name, cancellationToken) => !await IsNameDupplicated(name, cancellationToken))
                .WithMessage(x => $"Task detail name has been duplicated.");
            RuleFor(x => x.MeasurementUnit)
                .NotEmpty()
                .NotNull()
                .WithMessage("Measurement Unit cannot be null or empty.");
            RuleFor(x => x.Value)
                .NotEmpty()
                .NotNull()
                .WithMessage("Value cannot be null or empty");
        }

        private async Task<bool> IsNameDupplicated(string name, CancellationToken cancellationToken)
            => await _taskAttributeRepository.AnyAsync(x => x.Name.ToLower().Equals(name.ToLower()), cancellationToken);
    }
}
