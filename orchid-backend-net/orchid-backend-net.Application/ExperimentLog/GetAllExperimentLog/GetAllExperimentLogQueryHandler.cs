using AutoMapper;
using MediatR;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.ExperimentLog.GetAllExperimentLog
{
    public class GetAllExperimentLogQueryHandler : IRequestHandler<GetAllExperimentLogQuery, PageResult<ExperimentLogDTO>>
    {
        private readonly IExperimentLogRepository _experimentLogRepository;
        private readonly IMapper _mapper;
        public GetAllExperimentLogQueryHandler(IExperimentLogRepository experimentLogRepository, IMapper mapper)
        {
            _experimentLogRepository = experimentLogRepository;
            _mapper = mapper;
        }

        public async Task<PageResult<ExperimentLogDTO>> Handle(GetAllExperimentLogQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._experimentLogRepository.FindAllAsync( request.PageNumber, request.PageSize, cancellationToken);
                if (result.Count() == 0)
                    throw new NotFoundException("Not found any ExperimentLog in the system.");
                return PageResult<ExperimentLogDTO>.Create(
                    totalCount: result.TotalCount,
                    pageCount: result.PageSize,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize,
                    data: result.MapToExperimentLogDTOList(_mapper)
                    );

            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
