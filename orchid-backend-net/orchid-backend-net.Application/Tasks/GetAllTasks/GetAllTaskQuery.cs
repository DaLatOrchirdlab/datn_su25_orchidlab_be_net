using MediatR;
using orchid_backend_net.Application.Common.Extension;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Tasks.GetAllTasks
{
    public class GetAllTaskQuery : IRequest<PageResult<GetAllTaskQueryDto>>, IQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? TechnicianId { get; set; }
        public string? ResearcherId { get; set; }
        public GetAllTaskQuery(int pagenumber, int pagesize, string? technicianId, string? researcherId)
        {
            this.PageNumber = pagenumber;
            this.PageSize = pagesize;
            TechnicianId = technicianId;
            ResearcherId = researcherId;
        }
        public GetAllTaskQuery() { }
    }

    internal class GetAllTaskQueryHandler(ITaskRepository taskRepository) : IRequestHandler<GetAllTaskQuery, PageResult<GetAllTaskQueryDto>>
    {
        public async Task<PageResult<GetAllTaskQueryDto>> Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<Domain.Entities.Tasks> queryOptions(IQueryable<Domain.Entities.Tasks> query)
                {
                    query = query.Where(x => x.Status != 5);
                    if (!string.IsNullOrWhiteSpace(request.TechnicianId))
                        query = query.Where(x => x.Assigns.Any(assign => assign.TechnicianID == request.TechnicianId));
                    if (!string.IsNullOrWhiteSpace(request.ResearcherId))
                        query = query.Where(x => x.Researcher.Equals(request.ResearcherId));
                    return query;
                }
                var task = await taskRepository.FindAllProjectToAsync<GetAllTaskQueryDto>(
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
