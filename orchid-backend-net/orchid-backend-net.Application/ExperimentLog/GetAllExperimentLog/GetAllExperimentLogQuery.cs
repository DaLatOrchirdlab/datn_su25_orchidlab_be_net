using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.Common.Pagination;

namespace orchid_backend_net.Application.ExperimentLog.GetAllExperimentLog
{
    public class GetAllExperimentLogQuery : IRequest<PageResult<ExperimentLogDTO>>, IQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }
        public string? SearchTerm { get; set; }
        public GetAllExperimentLogQuery(int pageNumber, int pageSize, string filter, string searchTerm)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Filter = filter;
            this.SearchTerm = searchTerm;
        }
        public GetAllExperimentLogQuery() { }

    }
}
