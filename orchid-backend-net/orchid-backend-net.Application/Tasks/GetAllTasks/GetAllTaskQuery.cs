using MediatR;
using orchid_backend_net.Application.Common.Extension;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Domain.IRepositories;

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

    internal class GetAllTaskQueryHandler(ITaskRepository taskRepository) : IRequestHandler<GetAllTaskQuery, PageResult<TaskDTO>>
    {
        public async Task<PageResult<TaskDTO>> Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<Domain.Entities.Tasks> queryOptions(IQueryable<Domain.Entities.Tasks> query)
                {
                    query = query.Where(x => x.Status != 5);
                    return query;
                }
                var task = await taskRepository.FindAllProjectToAsync<TaskDTO>(
                    pageNo: request.PageNumber,
                    pageSize: request.PageSize,
                    queryOptions: queryOptions,
                    cancellationToken: cancellationToken);
                return task.ToAppPageResult();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
