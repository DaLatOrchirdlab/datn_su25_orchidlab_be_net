using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.Sample.CreateSample;
using orchid_backend_net.Application.TaskAttribute.CreateTaskAttribute;
using orchid_backend_net.Application.Tasks.CreateTask;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.ExperimentLog.CreateExperimentLog
{
    public class CreateExperimentLogCommand(string methodID, string description, string tissueCultureBatchID,
        List<string> hybridization, int numberOfSample, string name, List<string> technicianIds) : IRequest<string>, ICommand
    {
        public string Name { get; set; } = name;
        public string MethodID { get; set; } = methodID;
        public string Description { get; set; } = description;
        public string TissueCultureBatchID { get; set; } = tissueCultureBatchID;
        public List<string> Hybridization { get; set; } = hybridization;
        public int NumberOfSample { get; set; } = numberOfSample;
        public List<string> TechnicianID {  get; set; } = technicianIds;
    }

    internal class CreateExperimentLogCommandHandler(IExperimentLogRepository experimentLogRepository, IHybridizationRepository hybridizationRepository,
        ILinkedRepository linkedRepository, IStageRepository stageRepository, 
        ISender sender, ITaskTemplatesRepository taskTemplatesRepository,
        ITaskTemplateDetailsRepository taskTemplateDetailsRepository) : IRequestHandler<CreateExperimentLogCommand, string>
    {
        public async Task<string> Handle(CreateExperimentLogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //get all stage of the method
                var stages = await stageRepository.FindAllAsync(x => x.MethodID.Equals(request.MethodID), cancellationToken);
                //convert task template into task, convert task template detail into task attribute

                ExperimentLogs obj = new()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = request.Name,
                    MethodID = request.MethodID,
                    Description = request.Description,
                    TissueCultureBatchID = request.TissueCultureBatchID,
                    Status = 1
                };
                experimentLogRepository.Add(obj);
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
                for (int i = 0; i < request.NumberOfSample; i++)
                {
                    var idOfSample = await sender.Send(new CreateSampleCommand(name: $"Mẫu thí nghiệm số {i + 1} của {request.Name}", description: ""), cancellationToken);
                    var linked = new Linkeds()
                    {
                        ExperimentLogID = obj.ID,
                        SampleID = idOfSample,
                        ProcessStatus = 0
                    };
                    foreach(var stageTask in stages)
                    {
                        //list I: task template based on stage list
                        var taskTemplate = await taskTemplatesRepository.FindAllAsync(x => x.StageID.Equals(stageTask.ID), cancellationToken);

                        foreach(var taskTemplateAttribute in taskTemplate)
                        {
                            //list II: task template detail based on the task template
                            List<CreateTaskAttributeCommand> attributeCommand = new();
                            var taskTemplateDetails = await taskTemplateDetailsRepository.FindAllAsync(x => x.TaskTemplateID.Equals(taskTemplateAttribuete.ID), cancellationToken);
                            foreach(var taskDetail in taskTemplateDetails)
                            {
                                CreateTaskAttributeCommand attribute = new(name: taskDetail.Name, measurementUnit: taskDetail.Unit, value: (double)taskDetail.ExpectedValue);
                                attributeCommand.Add(attribute);
                            }
                            CreateTaskCommand taskCommand = new()
                            {
                                Name = taskTemplateAttribute.Name,
                                ExperimentLogID = obj.ID,
                                SampleID = idOfSample,
                                Description = taskTemplateAttribute.Description,
                                Start_date = DateTime.UtcNow,
                                End_date = DateTime.UtcNow.AddDays(1),
                                TechnicianID = request.TechnicianID,
                            };
                            await sender.Send(taskCommand, cancellationToken);
                        }
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
}
