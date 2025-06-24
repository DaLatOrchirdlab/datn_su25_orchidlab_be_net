using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.ExperimentLog.GetAllExperimentLog
{
    public class GetAllExperimentLogQuery : IRequest<PageResult<ExperimentLogDTO>>, IQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }
        public string? SearchTerm { get; set; }
        public GetAllExperimentLogQuery(int pagenumber, int pagesize, string filter, string searchTerm)
        {
            this.PageNumber = pagenumber;
            this.PageSize = pagesize;
            this.Filter = filter;
            this.SearchTerm = searchTerm;
        }
        public GetAllExperimentLogQuery() { }

    }
}
