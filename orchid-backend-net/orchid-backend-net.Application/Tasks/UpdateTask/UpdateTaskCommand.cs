using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Tasks.UpdateTask
{
    public class UpdateTaskCommand : IRequest<string>, ICommand
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public UpdateTaskCommand(string id, string name, string description, int status)
        {
            ID = id;
            Name = name;
            Description = description;
            Status = status;
        }
    }

    internal class UpdateTaskCommandHandler(ITaskRepository taskRepository, ICurrentUserService currentUserService) : IRequestHandler<UpdateTaskCommand, string>
    {
        public async Task<string> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var task = await taskRepository.FindAsync(x => x.ID == request.ID, cancellationToken);
                task.Name = request.Name;
                task.Description = request.Description;
                task.Status = request.Status;
                task.Update_date = DateTime.UtcNow;
                task.Update_by = currentUserService.UserId;
                return await taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Updated task ID :{request.ID}" : $"Failed update task with ID :{request.ID}";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
