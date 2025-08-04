using AutoMapper;
using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Report.GetAllReport
{
    public class GetAllReportQuery : IRequest<PageResult<ReportDTO>>, IQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllReportQuery() { }
        public GetAllReportQuery(int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }

    internal class GetAllReportQueryHandler(IReportRepository reportRepository, IMapper mapper) : IRequestHandler<GetAllReportQuery, PageResult<ReportDTO>>
    {

        public async Task<PageResult<ReportDTO>> Handle(GetAllReportQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await reportRepository.FindAllAsync(request.PageNumber, request.PageSize, cancellationToken);
                if (result == null)
                    throw new NotFoundException("Not found any Report in the system.");
                return PageResult<ReportDTO>.Create(
                    totalCount: result.TotalCount,
                    pageCount: result.PageSize,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize,
                    data: result.MapToReprotDTOList(mapper)
                    );
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
