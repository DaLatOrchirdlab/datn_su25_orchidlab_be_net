using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.Common.Pagination;

namespace orchid_backend_net.Application.Tasks.GetAllTasks
{
    public class GetAllTaskQuery : IRequest<PageResult<TaskDTO>>, IQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllTaskQuery(int pagenumber, int pagesize)
        {
            this.PageNumber = pagenumber;
            this.PageSize = pagesize;
        }
        public GetAllTaskQuery() { }
    }
}
