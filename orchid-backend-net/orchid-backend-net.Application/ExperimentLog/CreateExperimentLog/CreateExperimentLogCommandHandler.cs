using MediatR;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.ExperimentLog.CreateExperimentLog
{
    public class CreateExperimentLogCommandHandler : IRequestHandler<CreateExperimentLogCommand, string>
    {
        private readonly IExperimentLogRepository _experimentLogRepository;
        private readonly ITissueCultureBatchRepository _tissueCultureBatchRepository;
        private readonly IMethodRepository _methodRepository;
        private readonly IHybridizationRepository _hybridizationRepository;
        private readonly ISeedlingRepository _seedlingRepository;
        public CreateExperimentLogCommandHandler(IExperimentLogRepository experimentLogRepository, ITissueCultureBatchRepository tissueCultureBatchRepository, IMethodRepository methodRepository, IHybridizationRepository hybridizationRepository, ISeedlingRepository seedlingRepository)
        {
            _experimentLogRepository = experimentLogRepository;
            _tissueCultureBatchRepository = tissueCultureBatchRepository;
            _methodRepository = methodRepository;
            _hybridizationRepository = hybridizationRepository;
            _seedlingRepository = seedlingRepository;
        }

        public async Task<string> Handle(CreateExperimentLogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tissueculturebatch = await this._tissueCultureBatchRepository.FindAsync(x => x.ID.Equals(request.TissueCultureBatchID) && x.Status == true, cancellationToken);
                if (tissueculturebatch == null)
                    throw new NotFoundException($"Not found TissueCultureBatch with ID: {request.TissueCultureBatchID}");
                var method = await this._methodRepository.FindAsync(x => x.ID.Equals(request.MethodID), cancellationToken);
                if (method == null)
                    throw new NotFoundException($"Not found Method with ID :{request.MethodID}");
                Domain.Entities.ExperimentLogs obj = new Domain.Entities.ExperimentLogs()
                {
                    ID = Guid.NewGuid().ToString(),
                    MethodID = request.MethodID,
                    Description = request.Description,
                    TissueCultureBatchID = request.TissueCultureBatchID,
                    Status = 1
                };
                this._experimentLogRepository.Add(obj);
                foreach (var seedling in request.Hybridization)
                {
                    if ((await this._seedlingRepository.FindAsync(x => x.ID.Equals(seedling), cancellationToken)) != null)
                    {
                        Domain.Entities.Hybridizations parent = new Domain.Entities.Hybridizations()
                        {
                            ID = Guid.NewGuid().ToString(),
                            ExperimentLogID = obj.ID,
                            ParentID = seedling,
                            Status = true,
                            //IsMother = request.MotherID.ToLower().Equals(seedling.ToLower())
                        };
                        this._hybridizationRepository.Add(parent);
                    }
                }
                return await this._experimentLogRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Created ExperimentLog with ID: {obj.ID}" : "Failed to create ExperimentLog.";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
