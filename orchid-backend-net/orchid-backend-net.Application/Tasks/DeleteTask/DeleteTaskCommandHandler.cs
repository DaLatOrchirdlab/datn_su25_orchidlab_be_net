using AutoMapper;
using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Tasks.DeleteTask
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, string>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskAssignRepository _taskAssignRepository;
        private readonly IMapper _mapper;
        private readonly ITaskAttributeRepository _attributeRepository;
        private readonly ICurrentUserService _currentUserService;
        public DeleteTaskCommandHandler(ITaskRepository taskRepository, ITaskAssignRepository taskAssignRepository, IMapper mapper, ITaskAttributeRepository attributeRepository, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _taskRepository = taskRepository;
            _taskAssignRepository = taskAssignRepository;
            _mapper = mapper;
            _attributeRepository = attributeRepository;
        }

        public async Task<string> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var task = await this._taskRepository.FindAsync(x => x.ID.Equals(request.ID), cancellationToken);
                if (task == null)
                    throw new NotFoundException($"Not found task with ID: {request.ID}");
                task.Delete_date = DateTime.UtcNow;
                task.Delete_by = _currentUserService.UserId;
                return await _taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Task {task.Name} is deleted." : $"Failed to delete task {task.Name}";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
