using AutoMapper;
using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Sample.GetAllSample
{
    public class GetAllSampleQuery : IRequest<PageResult<SampleDTO>>, IQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllSampleQuery(int pagenumber, int pagesize)
        {
            this.PageNumber = pagenumber;
            this.PageSize = pagesize;
        }
        public GetAllSampleQuery() { }
    }

    internal class GetAllSampleQueryHandler(ISampleRepository sampleRepository, IMapper mapper) : IRequestHandler<GetAllSampleQuery, PageResult<SampleDTO>>
    {

        public async Task<PageResult<SampleDTO>> Handle(GetAllSampleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await sampleRepository.FindAllAsync(request.PageNumber, request.PageSize, cancellationToken);
                if (result.Count() == 0)
                    throw new NotFoundException("not found any Sample in the system.");
                return PageResult<SampleDTO>.Create(
                    totalCount: result.TotalCount,
                    pageCount: result.PageCount,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize,
                    data: result.MapToSampleDTOList(mapper)
                    );
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
