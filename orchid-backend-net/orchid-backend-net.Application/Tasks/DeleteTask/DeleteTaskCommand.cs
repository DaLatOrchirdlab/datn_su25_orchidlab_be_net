using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Tasks.DeleteTask
{
    public class DeleteTaskCommand : IRequest<string>, ICommand
    {

        public string ID { get; set; }
        public DeleteTaskCommand() { }
        public DeleteTaskCommand(string ID)
        {
            this.ID = ID;
        }

    }

    internal class DeleteTaskCommandHandler(ITaskRepository taskRepository, ITaskAssignRepository taskAssignRepository,
        ITaskAttributeRepository attributeRepository, ICurrentUserService currentUserService) : IRequestHandler<DeleteTaskCommand, string>
    {
        public async Task<string> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var task = await taskRepository.FindAsync(x => x.ID.Equals(request.ID), cancellationToken);
                var taskAttributes = await attributeRepository.FindAllAsync(x => x.TaskID.Equals(request.ID), cancellationToken);
                var taskAssign = await taskAssignRepository.FindAllAsync(x => x.TaskID.Equals(request.ID), cancellationToken);
                task.Delete_date = DateTime.UtcNow;
                task.Delete_by = currentUserService.UserId;
                foreach (var taskAttribute in taskAttributes)
                {
                    taskAttribute.Status = false;
                    attributeRepository.Update(taskAttribute);
                }
                foreach (var assigner in taskAssign)
                {
                    assigner.Status = false;
                    taskAssignRepository.Update(assigner);
                }
                return await taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Task {task.Name} is deleted." : $"Failed to delete task {task.Name}";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
