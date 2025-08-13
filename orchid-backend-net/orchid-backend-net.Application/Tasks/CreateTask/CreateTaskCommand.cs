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
        public string? StageID { get; set; }
        public string? SampleID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public bool IsDaily {  get; set; }
        public List<CreateTaskAttributeCommand> Attribute { get; set; }
        public List<string> TechnicianID { get; set; }
        public CreateTaskCommand(string name, string description, 
            DateTime start_date, DateTime end_date, List<CreateTaskAttributeCommand> attribute, 
            List<string> technicianID, string? experimentLogID, string? stageID, 
            string? sampleId, bool isdaily)
        {
            Name = name;
            Description = description;
            Start_date = start_date;
            End_date = end_date;
            Attribute = attribute;
            TechnicianID = technicianID;
            ExperimentLogID = experimentLogID;
            StageID = stageID;
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
                    ReportInformation = "aasdasdgse rgs ergs erg serg sdrgs ttg",
                    Url = "aeksrghbskdrgbilsdgyfbiarybfgukjsfdbgjksdfyg"
                };
                taskRepository.Add(task);

                foreach(var command in request.Attribute)
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

                var linkeds = new Domain.Entities.Linkeds
                {
                    //ExperimentLogID = request.ExperimentLogID,
                    //SampleID = request.SampleID,
                    TaskID = task.ID,
                    //StageID = request.StageID,
                    ProcessStatus = 0
                };
                if ((await sampleRepository.FindAsync(x => x.ID.Equals(request.SampleID), cancellationToken)) != null)
                {
                    linkeds.SampleID = request.SampleID;
                    linkeds.ExperimentLogID = null;
                    linkeds.StageID = request.StageID != null ? request.StageID : (await linkedRepository.FindAsync(x => x.SampleID.Equals(request.StageID) && x.StageID != null, cancellationToken)).StageID;
                }
                if ((await experimentLogRepository.FindAsync(x => x.ID.Equals(request.ExperimentLogID), cancellationToken)) != null)
                {
                    linkeds.SampleID = null;
                    linkeds.ExperimentLogID = request.ExperimentLogID;
                    linkeds.StageID = request.StageID != null ? request.StageID : (await linkedRepository.FindAsync(x => x.ExperimentLogID.Equals(request.ExperimentLogID) && x.StageID != null, cancellationToken)).StageID;
                }

                linkedRepository.Add(linkeds);

                return await taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Create task successfully." : "Failed to create task.";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
