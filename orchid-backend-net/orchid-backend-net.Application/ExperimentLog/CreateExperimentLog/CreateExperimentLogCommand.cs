using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.ExperimentLog.CreateExperimentLog
{
    public class CreateExperimentLogCommand : IRequest<string>, ICommand
    {
        public string MethodID { get; set; }
        public string Description { get; set; }
        public string TissueCultureBatchID { get; set; }
        public List<string> Hybridization { get; set; }
        public CreateExperimentLogCommand(string methodID, string description, string tissueCultureBatchID, List<string> hybridization)
        {
            MethodID = methodID;
            Description = description;
            TissueCultureBatchID = tissueCultureBatchID;
            Hybridization = hybridization;
        }
    }

    internal class CreateExperimentLogCommandHandler(IExperimentLogRepository experimentLogRepository, IHybridizationRepository hybridizationRepository, ISeedlingRepository seedlingRepository) : IRequestHandler<CreateExperimentLogCommand, string>
    {
        public async Task<string> Handle(CreateExperimentLogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ExperimentLogs obj = new()
                {
                    ID = Guid.NewGuid().ToString(),
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
                return await experimentLogRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Created ExperimentLog with ID: {obj.ID}" : "Failed to create ExperimentLog.";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
