using AutoMapper;
using MediatR;
using orchid_backend_net.Application.Common.Models;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Application.ExperimentLog;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Method.GetAllMethod
{
    public class GetAllMethodQueryHandler : IRequestHandler<GetAllMethodQuery, PageResult<MethodDTO>>
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
                var list = await this._methodRepository.FindAllAsync(x=> x.Status == true , request.PageNumber, request.PageSize, cancellationToken);
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
