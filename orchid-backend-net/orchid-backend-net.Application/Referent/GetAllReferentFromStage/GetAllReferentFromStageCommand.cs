using MediatR;
using orchid_backend_net.Application.Common.Extension;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Referent.GetAllReferentFromStage
{
    public class GetAllReferentFromStageCommand(string stageId, int pageNo, int pageSize) : IRequest<PageResult<ReferentDTO>>
    {
        public string StageId { get; set; } = stageId;
        public int PageNumber { get; set; } = pageNo;
        public int PageSize { get; set; } = pageSize;
    }

    internal class GetAllReferentFromStageCommandHandler(IReferentRepository referentRepository) : IRequestHandler<GetAllReferentFromStageCommand, PageResult<ReferentDTO>>
    {
        public async Task<PageResult<ReferentDTO>> Handle(GetAllReferentFromStageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<Referents> queryOptions(IQueryable<Referents> query)
                {
                    if (!string.IsNullOrWhiteSpace(request.StageId))
                        query = query.Where(x => x.StageID == request.StageId);
                    return query;
                }

                var referents = await referentRepository.FindAllProjectToAsync<ReferentDTO>(
                    pageNo: request.PageNumber,
                    pageSize: request.PageSize,
                    queryOptions: queryOptions,
                    cancellationToken: cancellationToken);
                return referents.ToAppPageResult();
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
