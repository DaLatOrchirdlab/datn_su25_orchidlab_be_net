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
                var stages = await stageRepository.FindAsync(x => x.MethodID.Equals(request.MethodID) && x.Status && x.Step == 1, cancellationToken);
                var taskTemplateDict = await taskTemplatesRepository.FindAllToDictionaryAsync<string, TaskTemplates>(template => template.StageID.Equals(stages.ID), key => key.ID, value => value, cancellationToken);


                //return await experimentLogRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Created ExperimentLog with ID: {obj.ID}" : "Failed to create ExperimentLog.";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
