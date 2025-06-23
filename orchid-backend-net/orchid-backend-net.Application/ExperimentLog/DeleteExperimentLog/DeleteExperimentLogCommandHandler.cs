using MediatR;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.ExperimentLog.DeleteExperimentLog
{
    public class DeleteExperimentLogCommandHandler : IRequestHandler<DeleteExperimentLogCommand, string>
    {
        private readonly IExperimentLogRepository _experimentLogRepository;
        public DeleteExperimentLogCommandHandler(IExperimentLogRepository experimentLogRepository)
        {
            _experimentLogRepository = experimentLogRepository;
        }
        public async Task<string> Handle(DeleteExperimentLogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var experimentLog = await this._experimentLogRepository.FindAsync(x => x.ID.Equals(request.ID), cancellationToken);
                if (experimentLog == null)
                    throw new NotFoundException($"Not found ExperimentLog with ID :{request.ID}");
                experimentLog.Status = 4;
                return await this._experimentLogRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Deleted ExperimentLog with ID :{request.ID}" : "Failed to delete ExperimentLog.";
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
