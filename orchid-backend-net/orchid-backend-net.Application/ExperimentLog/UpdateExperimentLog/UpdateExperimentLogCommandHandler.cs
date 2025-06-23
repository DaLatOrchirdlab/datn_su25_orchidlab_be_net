using MediatR;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.ExperimentLog.UpdateExperimentLog
{
    public class UpdateExperimentLogCommandHandler : IRequestHandler<UpdateExperimentLogCommand, string>
    {
        private readonly IExperimentLogRepository _experimentLogRepository;
        private readonly ITissueCultureBatchRepository _tissueCultureBatchRepository;
        private readonly IMethodRepository _methodRepository;
        private readonly IHybridizationRepository _hybridizationRepository;
        private readonly ISeedlingRepository _seedlingRepository;
        public UpdateExperimentLogCommandHandler(IExperimentLogRepository experimentLogRepository, ITissueCultureBatchRepository tissueCultureBatchRepository, IMethodRepository methodRepository, IHybridizationRepository hybridizationRepository, ISeedlingRepository seedlingRepository)
        {
            _experimentLogRepository = experimentLogRepository;
            _tissueCultureBatchRepository = tissueCultureBatchRepository;
            _methodRepository = methodRepository;
            _hybridizationRepository = hybridizationRepository;
            _seedlingRepository = seedlingRepository;
        }

        public async Task<string> Handle(UpdateExperimentLogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var experimentLog = await this._experimentLogRepository.FindAsync(x => x.ID.Equals(request.ID) && x.Status != 4, cancellationToken);
                if (experimentLog == null)
                    throw new NotFoundException($"Not found ExperimentLog with ID :{request.ID}");
                if (request.MethodID != null)
                {
                    experimentLog.MethodID = request.MethodID;
                }
                if (request.TissueCultureBatchID != null)
                {
                    experimentLog.TissueCultureBatchID = request.TissueCultureBatchID;
                }
                experimentLog.Description = request.Description;
                experimentLog.Status = request.Status;
                this._experimentLogRepository.Update(experimentLog);
                var hybridization = await this._hybridizationRepository.FindAllAsync(x => x.ExperimentLogID.Equals(request.ID) && x.Status == true, cancellationToken);
                foreach( var hybridizationItem in hybridization)
                {
                    foreach(var parent in request.Hybridization)
                    {
                        if (await this._seedlingRepository.FindAsync(x => x.ID.Equals(parent), cancellationToken) != null)
                            throw new NotFoundException($"Not fouond parent with ID :{parent}");
                        hybridizationItem.ParentID = parent;
                        this._hybridizationRepository.Update(hybridizationItem);
                    }
                }
                await this._hybridizationRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                return await this._experimentLogRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Updated ExperimentLog with ID :{request.ID}" : $"Failed to update ExperimentLog with ID :{request.ID}";
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
