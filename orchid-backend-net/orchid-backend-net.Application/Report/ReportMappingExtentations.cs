using AutoMapper;
using orchid_backend_net.Application.Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Report
{
    public static class ReportMappingExtentations
    {
        public static ReportDTO MapToReportDTO(this orchid_backend_net.Domain.Entities.Report report, IMapper mapper)
            => mapper.Map<ReportDTO>(report);
        public static List<ReportDTO> MapToReprotDTOList(this IEnumerable<orchid_backend_net.Domain.Entities.Report> reportList, IMapper mapper)
            => reportList.Select(x => x.MapToReportDTO(mapper)).ToList();
    }
}
