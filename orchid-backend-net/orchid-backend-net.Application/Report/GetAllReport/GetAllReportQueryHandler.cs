using AutoMapper;
using MediatR;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Application.Method;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Report.GetAllReport
{
    public class GetAllReportQueryHandler : IRequestHandler<GetAllReportQuery, PageResult<ReportDTO>>
    {
        private readonly IRepostRepository _reportRepository;
        private readonly IMapper _mapper;
        public GetAllReportQueryHandler(IRepostRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<PageResult<ReportDTO>> Handle(GetAllReportQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._reportRepository.FindAllAsync(request.PageNumber, request.PageSize, cancellationToken);
                if (result == null)
                    throw new NotFoundException("Not found any Report in the system.");
                return PageResult<ReportDTO>.Create(
                    totalCount: result.TotalCount,
                    pageCount: result.PageSize,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize,
                    data: result.MapToReprotDTOList(_mapper)
                    );
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
