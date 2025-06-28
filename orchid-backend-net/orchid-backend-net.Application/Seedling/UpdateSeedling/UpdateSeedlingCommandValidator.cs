using FluentValidation;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Seedling.UpdateSeedling
{
    public class UpdateSeedlingCommandValidator : AbstractValidator<UpdateSeedlingCommand>
    {
        private readonly ISeedlingRepository _seedlingRepository;
        public UpdateSeedlingCommandValidator(ISeedlingRepository seedlingRepository)
        {
            _seedlingRepository = seedlingRepository;
            Configure();
        }
        private void Configure()
        {
            RuleFor(x => x.SeedlingId)
                .NotEmpty().WithMessage("Seedling ID is required.")
                .MaximumLength(50).WithMessage("Seedling ID must not exceed 50 characters.");
            RuleFor(x => x.SeedlingId)
                .MustAsync(async (seedlingId, cancellation) => !await IsSeedlingExists(seedlingId))
                .WithMessage("Seedling with this ID does not exist.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
            RuleFor(x => x.DoB)
                .NotEmpty().WithMessage("Date of Birth is required.");
            RuleFor(x => x.Characteristics)
                .NotEmpty().WithMessage("At least one characteristic is required.");
        }

        private async Task<bool> IsSeedlingExists(string seedlingId)
        {
            return await _seedlingRepository.AnyAsync(x => x.ID.Equals(seedlingId));
        }
    }
}
