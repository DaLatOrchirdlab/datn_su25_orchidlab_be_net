using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.Sample.CreateSample;
using orchid_backend_net.Application.TaskAttribute.CreateTaskAttribute;
using orchid_backend_net.Application.Tasks.CreateTaskWhenCreateExperimentLog;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.ExperimentLog.CreateExperimentLog
{
    public class CreateExperimentLogCommand : IRequest<string>, ICommand
    {
        public string Name { get; set; }
        public string MethodID { get; set; }
        public string Description { get; set; }
        public string TissueCultureBatchID { get; set; }
        public List<string> Hybridization { get; set; }
        public int NumberOfSample { get; set; }
        public List<string> TechnicianID { get; set; }
    }

    internal class CreateExperimentLogCommandHandler(IExperimentLogRepository experimentLogRepository, IHybridizationRepository hybridizationRepository,
        ILinkedRepository linkedRepository, IStageRepository stageRepository,
        ISender sender, ITaskTemplatesRepository taskTemplatesRepository,
        ITaskTemplateDetailsRepository taskTemplateDetailsRepository,
        ICurrentUserService currentUserService) : IRequestHandler<CreateExperimentLogCommand, string>
    {
        public async Task<string> Handle(CreateExperimentLogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //get stage
                var stages = await stageRepository.FindAsync(x => x.MethodID.Equals(request.MethodID) && x.Status && x.Step == 1, cancellationToken);

                //get task template by stage
                var taskTemplates = await taskTemplatesRepository.FindAllAsync(template => template.StageID.Equals(stages.ID), cancellationToken);
                var taskTemplateIds = taskTemplates.Select(t => t.ID).ToHashSet();
                
                var taskTemplateDetails = await taskTemplateDetailsRepository.FindAllAsync(detail => taskTemplateIds.Contains(detail.TaskTemplateID), cancellationToken);
                var detailsGrouped = taskTemplateDetails
                    .GroupBy(d => d.TaskTemplateID)
                    .ToDictionary(g => g.Key, g => g.ToList());

                ExperimentLogs obj = new()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = request.Name,
                    CurrentStageID = stages.ID, // Assuming the first stage is the current stage
                    MethodID = request.MethodID,
                    Description = request.Description,
                    TissueCultureBatchID = request.TissueCultureBatchID,
                    Status = 0,
                    Create_date = DateTime.UtcNow,
                    Create_by = currentUserService.UserName,
                };

                foreach (var seedling in request.Hybridization)
                {
                    Hybridizations parent = new()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ExperimentLogID = obj.ID,
                        ParentID = seedling,
                        Status = true,
                    };
                    hybridizationRepository.Add(parent);
                }

                foreach (var taskTemplate in taskTemplates)
                {
                    var attributes = detailsGrouped.GetValueOrDefault(taskTemplate.ID, [])
                        .Select(x => new CreateTaskAttributeCommand(name: x.Name,
                            measurementUnit: x.Unit,
                            value: (double)x.ExpectedValue,
                            description: x.Description)
                        ).ToList();

                    var taskCommand = new CreateTaskWhenCreateExperimentLog(
                        name: taskTemplate.Name,
                        description: taskTemplate.Description,
                        start_date: DateTime.UtcNow,
                        end_date: DateTime.UtcNow.AddDays(1),
                        attribute: attributes,
                        technicianID: request.TechnicianID,
                        experimentLogID: obj.ID);

                    linkedRepository.Add(new Domain.Entities.Linkeds
                    {
                        ExperimentLogID = obj.ID,
                        TaskID = await sender.Send(taskCommand, cancellationToken),
                        StageID = obj.CurrentStageID,
                        ProcessStatus = 0
                    });
                }

                for (int i = 0; i < request.NumberOfSample; i++)
                {
                    var sampleId = await sender.Send(
                        new CreateSampleCommand($"Mẫu thí nghiệm số {i + 1} của {request.Name}", ""),
                        cancellationToken);

                    linkedRepository.Add(new Domain.Entities.Linkeds
                    {
                        ExperimentLogID = obj.ID,
                        SampleID = sampleId,
                        StageID = obj.CurrentStageID,
                        ProcessStatus = 0
                    });
                }

                return await experimentLogRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Created ExperimentLog with ID: {obj.ID}" : "Failed to create ExperimentLog.";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
