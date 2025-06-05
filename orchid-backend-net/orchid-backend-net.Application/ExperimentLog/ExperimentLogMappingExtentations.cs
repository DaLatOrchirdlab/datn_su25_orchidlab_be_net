using AutoMapper;
using orchid_backend_net.Application.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.ExperimentLog
{
    public static class ExperimentLogMappingExtentations
    {
        public static ExperimentLogDTO MapToExperimentLogDTO(this orchid_backend_net.Domain.Entities.ExperimentLog experimentLog, IMapper mapper)
            => mapper.Map<ExperimentLogDTO>(experimentLog);
        public static List<ExperimentLogDTO> MapToExperimentLogDTOList(this IEnumerable<orchid_backend_net.Domain.Entities.ExperimentLog> experimentLogList, IMapper mapper)
            => experimentLogList.Select(x => x.MapToExperimentLogDTO(mapper)).ToList();
    }
}
