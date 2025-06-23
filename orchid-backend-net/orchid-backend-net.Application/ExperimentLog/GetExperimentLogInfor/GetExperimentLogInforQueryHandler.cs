using AutoMapper;
using MediatR;
using orchid_backend_net.Domain.IRepositories;
using orchid_backend_net.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.ExperimentLog.GetExperimentLogInfor
{
    public class GetExperimentLogInforQueryHandler : IRequestHandler<GetExperimentLogInforQuery, ExperimentLogDTO>
    {
        private readonly IExperimentLogRepository _experimentLogRepository;
        private readonly IMapper _mapper;
        public GetExperimentLogInforQueryHandler(IExperimentLogRepository experimentLogRepository, IMapper mapper)
        {
            _experimentLogRepository = experimentLogRepository;
            _mapper = mapper;
        }

        public async Task<ExperimentLogDTO> Handle(GetExperimentLogInforQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var obj = await this._experimentLogRepository.FindAsync(x => x.ID.Equals(request.ID), cancellationToken);
                if (obj == null)
                    throw new NotFoundException($"Not found any ExperimentLog with ID:{request.ID} in the system");
                return obj.MapToExperimentLogDTO(_mapper);
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
