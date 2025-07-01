using AutoMapper;
using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Method.GetAllMethod
{
    public class GetAllMethodQuery : IRequest<PageResult<MethodDTO>>, IQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllMethodQuery() { }
        public GetAllMethodQuery(int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }

    internal class GetAllMethodQueryHandler : IRequestHandler<GetAllMethodQuery, PageResult<MethodDTO>>
    {
        private readonly IMethodRepository _methodRepository;
        private readonly IMapper _mapper;
        public GetAllMethodQueryHandler(IMethodRepository methodRepository, IMapper mapper)
        {
            _methodRepository = methodRepository;
            _mapper = mapper;
        }
        public async Task<PageResult<MethodDTO>> Handle(GetAllMethodQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var list = await this._methodRepository.FindAllAsync(x => x.Status == true, request.PageNumber, request.PageSize, cancellationToken);
                if (list.Count() == 0)
                    throw new NotFoundException("Not found any method in the system.");
                return PageResult<MethodDTO>.Create(
                    totalCount: list.TotalCount,
                    pageCount: list.PageSize,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize,
                    data: list.MapToMethodDTOList(_mapper)
                    );
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
