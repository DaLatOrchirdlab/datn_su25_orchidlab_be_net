using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.Sample.CreateSample;
using orchid_backend_net.Application.TaskAttribute.CreateTaskAttribute;
using orchid_backend_net.Application.Tasks.CreateTaskWhenCreateExperimentLog;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;

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
                //basically: when create experiment log => created task with task template

                //get stage
                var stages = await stageRepository.FindAllAsync(x => x.MethodID.Equals(request.MethodID), cancellationToken);
                var stageIds = stages.Select(x => x.ID).ToList();

                //get task template
                var taskTemplatesList = await taskTemplatesRepository.FindAllAsync(
                    x => stageIds.Contains(x.StageID),
                    cancellationToken);
                var taskTemplatesDict = taskTemplatesList
                    .GroupBy(x => x.StageID)
                    .ToDictionary(g => g.Key, g => g.ToList());
                var allTaskTemplateIDs = taskTemplatesList.Select(x => x.ID).ToList();

                //get task template detail
                var taskTemplateDetailsList = await taskTemplateDetailsRepository.FindAllAsync(x => allTaskTemplateIDs.Contains(x.TaskTemplateID), cancellationToken);
                var taskTemplateDetailsDict = taskTemplateDetailsList
                    .GroupBy(x => x.TaskTemplateID)
                    .ToDictionary(g => g.Key, g => g.ToList());

                //flatten Task Pipeline
                //if task about report => report for each sample
                //task not about report => el id which means for whole experiment log
                var taskPipelineInfos = stages
                    .SelectMany(stage =>
                        taskTemplatesDict.TryGetValue(stage.ID, out var templates)
                            ? templates.Select(template =>
                            {
                                var details = taskTemplateDetailsDict.TryGetValue(template.ID, out var templateDetails)
                                    ? templateDetails
                                    : new List<TaskTemplateDetails>();

                                return new TaskPipelineInfo(
                                    StageID: stage.ID,
                                    TaskTemplateID: template.ID,
                                    TaskTemplateName: template.Name,
                                    TaskTemplateDescription: template.Description,
                                    TaskTemplateDetails: details
                                );
                            })
                    : Enumerable.Empty<TaskPipelineInfo>()).ToList();


                //create experiment log
                ExperimentLogs obj = new()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = request.Name,
                    CurrentStageID = stageIds.FirstOrDefault(), // Assuming the first stage is the current stage
                    MethodID = request.MethodID,
                    Description = request.Description,
                    TissueCultureBatchID = request.TissueCultureBatchID,
                    Status = 1,
                    Create_date = DateTime.UtcNow,
                    Create_by = currentUserService.UserName,
                };
                
                experimentLogRepository.Add(obj);

                //create hybridzations
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

                //bulk Insert Linkeds - sample - task
                for (int i = 0; i < request.NumberOfSample; i++)
                {
                    var sampleId = await sender.Send(
                        new CreateSampleCommand($"Mẫu thí nghiệm số {i + 1} của {request.Name}", ""),
                        cancellationToken);

                    foreach (var pipeline in taskPipelineInfos)
                    {
                        var attributes = pipeline.TaskTemplateDetails
                            .Select(detail => new CreateTaskAttributeCommand(
                                name: detail.Name,
                                description: detail.Description,
                                measurementUnit: detail.Unit,
                                value: (double)detail.ExpectedValue
                            )).ToList();

                        var taskCommand = new CreateTaskWhenCreateExperimentLog(pipeline.TaskTemplateName + $" {obj.Name}", pipeline.TaskTemplateDescription, DateTime.UtcNow,
                            DateTime.UtcNow.AddDays(1), attributes, request.TechnicianID,
                            obj.ID, pipeline.StageID, sampleId);
                        var taskId = await sender.Send(taskCommand, cancellationToken);

                        linkedRepository.Add(new Domain.Entities.Linkeds
                        {
                            ExperimentLogID = obj.ID,
                            SampleID = sampleId,
                            TaskID = taskId,
                            ProcessStatus = 0
                        });
                    }
                }

                return await experimentLogRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Created ExperimentLog with ID: {obj.ID}" : "Failed to create ExperimentLog.";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }

    public record TaskPipelineInfo(
    string StageID,
    string TaskTemplateID,
    string TaskTemplateName,
    string TaskTemplateDescription,
    List<TaskTemplateDetails> TaskTemplateDetails
    );
}
