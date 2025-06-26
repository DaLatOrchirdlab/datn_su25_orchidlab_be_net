using AutoMapper;
using MediatR;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Sample.GetAllSample
{
    public class GetAllSampleQueryHandler : IRequestHandler<GetAllSampleQuery, PageResult<SampleDTO>>
    {
        private readonly ISampleRepository _sampleRepository;
        private readonly IMapper _mapper;
        public GetAllSampleQueryHandler(ISampleRepository sampleRepository, IMapper mapper)
        {
            _sampleRepository = sampleRepository;
            _mapper = mapper;
        }

        public async Task<PageResult<SampleDTO>> Handle(GetAllSampleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._sampleRepository.FindAllAsync(request.PageNumber, request.PageSize, cancellationToken);
                if (result.Count() == 0)
                    throw new NotFoundException("not found any Sample in the system.");
                return PageResult<SampleDTO>.Create(
                    totalCount: result.TotalCount,
                    pageCount: result.PageCount,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize,
                    data: result.MapToSampleDTOList(_mapper)
                    );
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
