using AutoMapper;
using orchid_backend_net.Application.ExperimentLog;
using orchid_backend_net.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.LabRoom
{
    public static class LabRoomMappingExtentations
    {
        public static LabRoomDTO MapToLabRoomDTO(this orchid_backend_net.Domain.Entities.LabRoom labRoom, IMapper mapper)
            => mapper.Map<LabRoomDTO>(labRoom);
        public static List<LabRoomDTO> MapToLabRoomDTOList(this IEnumerable<orchid_backend_net.Domain.Entities.LabRoom> labRoomList, IMapper mapper)
            => labRoomList.Select(x => x.MapToLabRoomDTO(mapper)).ToList();
    }
}
