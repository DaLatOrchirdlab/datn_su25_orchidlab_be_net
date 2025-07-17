using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.TaskAssign.UpdateTaskAssign;
using orchid_backend_net.Application.TaskAttribute.CreateTaskAttribute;
using orchid_backend_net.Application.TaskAttribute.UpdateTaskAttribute;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Tasks.UpdateTask
{
    public class UpdateTaskCommand : IRequest<string>, ICommand
    {
        public string ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        public List<UpdateTaskAssignCommand>? TaskAssignUpdate { get; set; }
        public List<UpdateTaskAttributeCommand>? TaskAttributeUpdate { get; set; }
        public List<CreateTaskAttributeCommand>? TaskAttributeCreate { get; set; }
        public UpdateTaskCommand(string id, string? name, string? description,
            int? status, List<UpdateTaskAssignCommand>? assign, List<UpdateTaskAttributeCommand>? attribute, 
            List<CreateTaskAttributeCommand>? createTaskAttributeCommands)
        {
            ID = id;
            Name = name;
            Description = description;
            Status = status;
            TaskAssignUpdate = assign;
            TaskAttributeUpdate = attribute;
            TaskAttributeCreate = createTaskAttributeCommands;
        }
    }

    internal class UpdateTaskCommandHandler(ITaskRepository taskRepository, ITaskAssignRepository taskAssignRepository,
        ITaskAttributeRepository taskAttributeRepository, ICurrentUserService currentUserService,
        ISender sender) : IRequestHandler<UpdateTaskCommand, string>
    {
        public async Task<string> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var task = await taskRepository.FindAsync(x => x.ID == request.ID, cancellationToken);
                task.Name = request.Name ?? task.Name;
                task.Description = request.Description ?? task.Description;
                task.Status = request.Status ?? task.Status;
                task.Update_date = DateTime.UtcNow;
                task.Update_by = currentUserService.UserId;
                //check task assign list to update
                var taskAssignList = await taskAssignRepository.FindAllAsync(x => x.TaskID.Equals(request.ID), cancellationToken);
                foreach (var updateTaskAssignCommand in request.TaskAssignUpdate)
                {
                    if (taskAssignList.Any(x => x.ID.Equals(updateTaskAssignCommand.Id) && IsDifferentTechnician(x, updateTaskAssignCommand)))
                        await sender.Send(updateTaskAssignCommand, cancellationToken);
                }

                //check task attribute list to update
                var taskAttributeList = await taskAttributeRepository.FindAllAsync(x => x.TaskID.Equals(request.ID), cancellationToken);
                foreach (var updateTaskAttributeCommand in request.TaskAttributeUpdate)
                {
                    if (taskAttributeList.Any(x => x.ID.Equals(updateTaskAttributeCommand.Id) && IsDifferentAttribute(x, updateTaskAttributeCommand)))
                        await sender.Send(updateTaskAttributeCommand, cancellationToken);
                }

                if(request.TaskAttributeCreate != null && request.TaskAttributeCreate.Count > 0)
                {
                    foreach(var createTaskAttributeCommand in request.TaskAttributeCreate)
                    {
                        createTaskAttributeCommand.TaskId = task.ID;
                        await sender.Send(createTaskAttributeCommand,cancellationToken);
                    }
                }

                return await taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Updated task ID :{request.ID}" : $"Failed update task with ID :{request.ID}";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        private bool IsDifferentAttribute(TaskAttributes x, UpdateTaskAttributeCommand command)
            => x.Name != command.Name
            || x.MeasurementUnit != command.MeasurementUnit
            || x.Value != command.Value;

        private bool IsDifferentTechnician(Domain.Entities.TasksAssign x, UpdateTaskAssignCommand command)
            => x.TechnicianID != command.TechnicianId;
    }
}
