using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.TaskAssign.CreateTaskAssign;
using orchid_backend_net.Application.TaskAttribute.CreateTaskAttribute;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Tasks.CreateTask
{
    public class CreateTaskCommand : IRequest<string>, ICommand
    {
        public string? ExperimentLogID { get; set; }
        public string? SampleID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public bool IsDaily { get; set; }
        public List<CreateTaskAttributeCommand> Attribute { get; set; }
        public List<string> TechnicianID { get; set; }
        public CreateTaskCommand(string name, string description,
            DateTime start_date, DateTime end_date, List<CreateTaskAttributeCommand> attribute,
            List<string> technicianID, string? experimentLogID,
            string? sampleId, bool isdaily)
        {
            Name = name;
            Description = description;
            Start_date = start_date;
            End_date = end_date;
            Attribute = attribute;
            TechnicianID = technicianID;
            ExperimentLogID = experimentLogID;
            SampleID = sampleId;
            IsDaily = isdaily;
        }
        public CreateTaskCommand() { }
    }

    internal class CreateTaskCommandHandler(ITaskRepository taskRepository, ILinkedRepository linkedRepository,
        IExperimentLogRepository experimentLogRepository, ISampleRepository sampleRepository,
        ICurrentUserService currentUserService, ISender sender) : IRequestHandler<CreateTaskCommand, string>
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
                    Researcher = currentUserService.UserName,
                    IsDaily = request.IsDaily,
                    Status = 0,
                };
                taskRepository.Add(task);

                foreach (var command in request.Attribute)
                {
                    command.TaskId = task.ID;
                    await sender.Send(command, cancellationToken);
                }

                foreach (var technicianID in request.TechnicianID)
                {
                    CreateTaskAssignCommand assignCommand = new(technicianId: technicianID);
                    assignCommand.TechnicianId = technicianID;
                    assignCommand.TaskId = task.ID;
                    await sender.Send(assignCommand, cancellationToken);
                }

                if ((await sampleRepository.FindAsync(x => x.ID.Equals(request.SampleID), cancellationToken)) != null)
                {
                    var linkedExperimentLog = await linkedRepository.FindAllAsync(x => x.SampleID.Equals(request.SampleID) && x.StageID != null, cancellationToken);
                    var experimentLogId = linkedExperimentLog.Select(x => x.ExperimentLogID)
                        .FirstOrDefault(x => x != null);
                    var experimentLog = await experimentLogRepository.FindAsync(x => x.ID.Equals(experimentLogId), cancellationToken);

                    var linkeds = new Domain.Entities.Linkeds
                    {
                        SampleID = request.SampleID,
                        ExperimentLogID = experimentLogId,
                        TaskID = task.ID,
                        StageID = experimentLog.CurrentStageID,
                        ProcessStatus = 0
                    };
                    linkedRepository.Add(linkeds);
                }

                if ((await experimentLogRepository.FindAsync(x => x.ID.Equals(request.ExperimentLogID), cancellationToken)) != null)
                {
                    var experimentLog = await experimentLogRepository.FindAsync(x => x.ID.Equals(request.ExperimentLogID), cancellationToken);
                    var linkedsSampleDuplicated = await linkedRepository.FindAllAsync(x => x.ExperimentLogID.Equals(request.ExperimentLogID), cancellationToken);
                    var uniqueLinkedsSample = linkedsSampleDuplicated.Select(linkeds => linkeds.SampleID)
                        .Where(sample => sample != null)
                        .Distinct()
                        .ToList();
                    var linkeds = new Domain.Entities.Linkeds
                    {
                        ExperimentLogID = request.ExperimentLogID,
                        TaskID = task.ID,
                        StageID = experimentLog.CurrentStageID,
                        ProcessStatus = 0
                    };
                    linkedRepository.Add(linkeds);
                }
                return await taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Create task successfully." : "Failed to create task.";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
