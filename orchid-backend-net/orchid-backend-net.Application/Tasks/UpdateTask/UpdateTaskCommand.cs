using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.TaskAssign.UpdateTaskAssign;
using orchid_backend_net.Application.TaskAttribute.CreateTaskAttribute;
using orchid_backend_net.Application.TaskAttribute.UpdateTaskAttribute;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Tasks.UpdateTask
{
    public class UpdateTaskCommand(string id, string? name, string? description,
        int? status, List<UpdateTaskAssignCommand>? taskAssignUpdate, List<UpdateTaskAttributeCommand>? taskAttributeUpdate,
        List<CreateTaskAttributeCommand>? taskAttributeCreate) : IRequest<string>, ICommand
    {
        public string ID { get; set; } = id;
        public string? Name { get; set; } = name;
        public string? Description { get; set; } = description;
        public int? Status { get; set; } = status;
        public List<UpdateTaskAssignCommand>? TaskAssignUpdate { get; set; } = taskAssignUpdate;
        public List<UpdateTaskAttributeCommand>? TaskAttributeUpdate { get; set; } = taskAttributeUpdate;
        public List<CreateTaskAttributeCommand>? TaskAttributeCreate { get; set; } = taskAttributeCreate;
    }

    //refactor again base on solid
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
                task.Update_by = currentUserService.UserName;

                //check task assign list to update
                if (request.TaskAssignUpdate != null && request.TaskAssignUpdate.Count > 0)
                {
                    var taskAssignList = await taskAssignRepository.FindAllAsync(x => x.TaskID.Equals(request.ID), cancellationToken);
                    foreach (var updateTaskAssignCommand in request.TaskAssignUpdate)
                    {
                        if (taskAssignList.Any(x => x.ID.Equals(updateTaskAssignCommand.Id) && IsDifferentTechnician(x, updateTaskAssignCommand)))
                            await sender.Send(updateTaskAssignCommand, cancellationToken);
                    }
                }

                //check task attribute list to update
                if (request.TaskAttributeUpdate != null && request.TaskAttributeUpdate.Count > 0)
                {
                    var taskAttributeList = await taskAttributeRepository.FindAllAsync(x => x.TaskID.Equals(request.ID), cancellationToken);
                    foreach (var updateTaskAttributeCommand in request.TaskAttributeUpdate)
                    {
                        if (taskAttributeList.Any(x => x.ID.Equals(updateTaskAttributeCommand.Id) && IsDifferentAttribute(x, updateTaskAttributeCommand)))
                            await sender.Send(updateTaskAttributeCommand, cancellationToken);
                    }
                }

                //if researcher created a new task attribute => this will handle the case
                if (request.TaskAttributeCreate != null && request.TaskAttributeCreate.Count > 0)
                {
                    foreach (var createTaskAttributeCommand in request.TaskAttributeCreate)
                    {
                        createTaskAttributeCommand.TaskId = task.ID;
                        await sender.Send(createTaskAttributeCommand, cancellationToken);
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
