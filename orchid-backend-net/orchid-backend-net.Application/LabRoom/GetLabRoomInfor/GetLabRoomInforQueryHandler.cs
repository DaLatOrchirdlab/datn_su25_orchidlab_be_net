using AutoMapper;
using MediatR;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.LabRoom.GetLabRoomInfor
{
    public class GetLabRoomInforQueryHandler : IRequestHandler<GetLabRoomInforQuery, LabRoomDTO>
    {
        private readonly ILabRoomRepository _labRoomRepository;
        private readonly IMapper _mapper;
        public GetLabRoomInforQueryHandler(ILabRoomRepository labRoomRepository, IMapper mapper)
        {
            _labRoomRepository = labRoomRepository;
            _mapper = mapper;
        }

        public async Task<LabRoomDTO> Handle(GetLabRoomInforQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var labroom = await this._labRoomRepository.FindAsync(x => x.ID.Equals(request.ID) && x.Status == true, cancellationToken);
                if (labroom == null)
                    throw new NotFoundException($"Not found LabRoom with ID :{request.ID}");
                return labroom.MapToLabRoomDTO(_mapper);
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
