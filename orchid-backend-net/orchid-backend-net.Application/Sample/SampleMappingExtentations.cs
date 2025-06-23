using AutoMapper;
using orchid_backend_net.Application.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Sample
{
    public static class SampleMappingExtentations
    {
        public static SampleDTO MapToSampleDTO(this orchid_backend_net.Domain.Entities.Sample obj, IMapper mapper)
            => mapper.Map<SampleDTO>(obj);
        public static List<SampleDTO> MapToSampleDTOList(this IEnumerable<orchid_backend_net.Domain.Entities.Sample> objList, IMapper mapper)
            => objList.Select(x => x.MapToSampleDTO(mapper)).ToList();
    }
}
