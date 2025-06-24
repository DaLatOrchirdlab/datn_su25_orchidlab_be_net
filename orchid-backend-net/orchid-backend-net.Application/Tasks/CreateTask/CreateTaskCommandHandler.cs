using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Tasks.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, string>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskAssignRepository _taskAssignRepository;
        private readonly ITaskAttributeRepository _taskAttributeRepository;
        private readonly ICurrentUserService _currentUserService;
        public CreateTaskCommandHandler(ITaskRepository taskRepository, ITaskAssignRepository taskAssignRepository, ITaskAttributeRepository taskAttributeRepository, ICurrentUserService currentUserService)
        {
            _taskRepository = taskRepository;
            _taskAssignRepository = taskAssignRepository;
            _taskAttributeRepository = taskAttributeRepository;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var task = new Domain.Entities.Task()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = request.Name,
                    Create_at = DateTime.UtcNow,
                    Start_date = request.Start_date,
                    End_date = request.End_date,
                    Description = request.Description,
                    Researcher = _currentUserService.UserId,
                    Status = 0,
                };
                _taskRepository.Add(task);
                foreach (var technician in request.TechnicianID)
                {
                    var taskAssign = new Domain.Entities.TaskAssign()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Status = 1,
                        TaskID = task.ID,
                        TechnicianID = technician
                    };
                    _taskAssignRepository.Add(taskAssign);
                }
                await _taskAssignRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                foreach (var item in request.Attribute)
                {
                    var taskAttribute = new Domain.Entities.TaskAttribute()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Description = item.Description,
                        Status = true,
                        TaskID = task.ID,
                        Value = item.Value
                    };
                    _taskAttributeRepository.Add(taskAttribute);
                }
                await this._taskAttributeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                return await this._taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Create task successfully." : "Failed to create task.";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
