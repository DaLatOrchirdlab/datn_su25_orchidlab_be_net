using AutoMapper;
using orchid_backend_net.Application.ExperimentLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Method
{
    public static class MethodMappingExtentations
    {
        public static MethodDTO MapToMethodDTO(this orchid_backend_net.Domain.Entities.Method method, IMapper mapper)
            => mapper.Map<MethodDTO>(method);
        public static List<MethodDTO> MapToMethodDTOList(this IEnumerable<orchid_backend_net.Domain.Entities.Method> methodList, IMapper mapper)
            => methodList.Select(x => x.MapToMethodDTO(mapper)).ToList();
    }
}
