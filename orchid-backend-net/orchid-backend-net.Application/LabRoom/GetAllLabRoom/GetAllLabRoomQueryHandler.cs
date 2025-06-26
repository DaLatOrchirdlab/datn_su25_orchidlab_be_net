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

namespace orchid_backend_net.Application.LabRoom.GetAllLabRoom
{
    public class GetAllLabRoomQueryHandler : IRequestHandler<GetAllLabRoomQuery, PageResult<LabRoomDTO>>
    {
        private readonly ILabRoomRepository _labRoomRepository;
        private readonly IMapper _mapper;
        public GetAllLabRoomQueryHandler(ILabRoomRepository labRoomRepository, IMapper mapper)
        {
            _labRoomRepository = labRoomRepository;
            _mapper = mapper;
        }

        public async Task<PageResult<LabRoomDTO>> Handle(GetAllLabRoomQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._labRoomRepository.FindAllAsync(request.PageNumber, request.PageSize, cancellationToken);
                if (result == null)
                    throw new NotFoundException("Not found any LabRoom in the system.");
                return PageResult<LabRoomDTO>.Create(
                                totalCount: result.TotalCount,
                                pageCount: result.PageSize,
                                pageNumber: request.PageNumber,
                                pageSize: request.PageSize,
                                data: result.MapToLabRoomDTOList(_mapper)
                                );
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
