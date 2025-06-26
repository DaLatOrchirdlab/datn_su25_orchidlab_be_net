using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Sample.GetAllSampleInExperimentLog
{
    public class GetAllSampleInExperimentLogQuery : IRequest<PageResult<SampleDTO>>, IQuery
    {
        public string ExperimentLogID { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllSampleInExperimentLogQuery(string experimentLogID, int pageNumber, int pageSize)
        {
            ExperimentLogID = experimentLogID;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public GetAllSampleInExperimentLogQuery() { }
    }
}
