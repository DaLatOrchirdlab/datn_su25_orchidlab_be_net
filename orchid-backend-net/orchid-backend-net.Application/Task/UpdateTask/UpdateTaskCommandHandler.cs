using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Task.UpdateTask
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, string>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICurrentUserService _currentUserService;
        public UpdateTaskCommandHandler(ITaskRepository taskRepository, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _taskRepository = taskRepository;
        }
        public async Task<string> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var task = await _taskRepository.FindAsync(x => x.ID == request.ID, cancellationToken);
                if (task == null)
                    throw new NotFoundException($"Not found any task with ID :{request.ID}");
                task.Name = request.Name;
                task.Description = request.Description;
                task.Status = request.Status;
                task.Update_date = DateTime.UtcNow;
                task.Update_by = _currentUserService.UserId;
                return await _taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Updated task ID :{request.ID}" : $"Failed update task with ID :{request.ID}";
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
