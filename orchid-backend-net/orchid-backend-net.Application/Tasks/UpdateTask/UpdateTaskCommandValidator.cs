using FluentValidation;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Tasks.UpdateTask
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskAssignRepository _taskAssignRepository;
        private readonly ITaskAttributeRepository _taskAttributeRepository;
        public UpdateTaskCommandValidator(ITaskRepository taskRepository, 
            ITaskAssignRepository taskAssignRepository, 
            ITaskAttributeRepository taskAttributeRepository)
        {
            _taskRepository = taskRepository;
            _taskAssignRepository = taskAssignRepository;
            _taskAttributeRepository = taskAttributeRepository;
            Configuration();
        }

        void Configuration()
        {
            RuleFor(x => x.ID)
                .NotEmpty()
                .NotNull()
                .WithMessage("ID can not null");
            RuleFor(x => x.ID)
                .MustAsync(async (id, cancellationToken) => await IsTaskExist(id, cancellationToken))
                .WithMessage(x => $"Not found any task with ID :{x.ID}");
            RuleFor(x => x.Status)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Your status is out of scope.");
            RuleFor(x => x.Status)
                .LessThanOrEqualTo(5)
                .WithMessage("Your status is out of scope.");
        }
        private async Task<bool> IsTaskExist(string id, CancellationToken cancellationToken)
            => await _taskRepository.AnyAsync(x => x.ID.Equals(id), cancellationToken);
    }
}
