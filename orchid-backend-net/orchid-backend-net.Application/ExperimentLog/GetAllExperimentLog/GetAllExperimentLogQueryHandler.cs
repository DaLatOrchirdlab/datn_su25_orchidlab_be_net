using MediatR;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.ExperimentLog.GetAllExperimentLog
{
    public class GetAllExperimentLogQueryHandler(IExperimentLogRepository experimentLogRepository) : IRequestHandler<GetAllExperimentLogQuery, PageResult<ExperimentLogDTO>>
    {

        public async Task<PageResult<ExperimentLogDTO>> Handle(GetAllExperimentLogQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<Domain.Entities.ExperimentLog> queryOptions(IQueryable<Domain.Entities.ExperimentLog> query)
                {
                    if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                        query = query.Where(x => x.TissueCultureBatch.Name.Contains(request.SearchTerm));

                    if (!string.IsNullOrWhiteSpace(request.Filter))
                        query = query.Where(x => x.MethodID == request.Filter);

                    return query;
                }

                var experimentLogs = await experimentLogRepository.FindAllProjectToAsync<ExperimentLogDTO>(
                    pageNo: request.PageNumber,
                    pageSize: request.PageSize,
                    queryOptions: queryOptions,
                    cancellationToken: cancellationToken);

                return experimentLogs.ToAppPageResult();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
