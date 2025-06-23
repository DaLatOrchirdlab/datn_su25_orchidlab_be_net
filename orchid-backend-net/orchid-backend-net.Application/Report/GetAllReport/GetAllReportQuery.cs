using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Report.GetAllReport
{
    public class GetAllReportQuery : IRequest<PageResult<ReportDTO>>, IQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllReportQuery() { }
        public GetAllReportQuery(int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}
