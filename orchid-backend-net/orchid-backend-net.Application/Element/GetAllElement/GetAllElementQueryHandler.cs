using AutoMapper;
using MediatR;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Element.GetAllElement
{
    public class GetAllElementQueryHandler(IElementRepositoty elementRepositoty, IMapper mapper) : IRequestHandler<GetAllElementQuery, PageResult<ElementDTO>>
    {
        private readonly IElementRepositoty _elementRepositoty;
        private readonly IMapper _mapper;
        public async Task<PageResult<ElementDTO>> Handle(GetAllElementQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var list = await this._elementRepositoty.FindAllAsync(x => x.Status == true, request.PageNumber, request.PageSize, cancellationToken);
                if (list.Count() == 0)
                    throw new NotFoundException("there're no element in the system.");
                return PageResult<ElementDTO>.Create(totalCount: list.TotalCount,
                    pageCount: list.PageCount,
                    pageSize: list.PageSize,
                    pageNumber: list.PageNo,
                    data: list.MapToElementDTOList(_mapper)
                    );
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
