using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Tasks.CreateTask
{
    public class CreateTaskCommand : IRequest<string>, ICommand
    {
        public string Researcher { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public DateTime Create_at { get; set; }
        public int Status { get; set; }
        public Domain.Enums.TaskStatus StatusEnum { get; set; }
        public List<TaskAttributes> Attribute { get; set; }
        public List<string> TechnicianID { get; set; }
        public CreateTaskCommand(string researcher, string name, string description, DateTime start_date, DateTime end_date, DateTime create_at, int status, List<TaskAttributes> attribute, List<string> technicianID, Domain.Enums.TaskStatus StatusEnum)
        {
            Researcher = researcher;
            Name = name;
            Description = description;
            Start_date = start_date;
            End_date = end_date;
            Create_at = create_at;
            Attribute = attribute;
            TechnicianID = technicianID;
            Status = status;
            this.StatusEnum = StatusEnum;
        }
        public CreateTaskCommand() { }
    }

    internal class CreateTaskCommandHandler(ITaskRepository taskRepository, ITaskAssignRepository taskAssignRepository, 
        ITaskAttributeRepository taskAttributeRepository, ICurrentUserService currentUserService) : IRequestHandler<CreateTaskCommand, string>
    {

        public async Task<string> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var task = new Domain.Entities.Tasks()
                {
                    Name = request.Name,
                    Create_at = DateTime.UtcNow,
                    Start_date = request.Start_date,
                    End_date = request.End_date,
                    Description = request.Description,
                    Researcher = currentUserService.UserId,
                    Status = 0,
                };
                taskRepository.Add(task);
                foreach (var technician in request.TechnicianID)
                {
                    var taskAssign = new TasksAssign()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Status = true,
                        TaskID = task.ID,
                        TechnicianID = technician
                    };
                    taskAssignRepository.Add(taskAssign);
                }
                await taskAssignRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                await taskAttributeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                return await taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Create task successfully." : "Failed to create task.";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
