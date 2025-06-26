using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Report.GetReportInfor
{
    public class GetReportInforQuery : IRequest<ReportDTO>, IQuery
    {
        public string ID {  get; set; }
        public GetReportInforQuery(string id)
        {
            ID = id;
        }
    }
}
