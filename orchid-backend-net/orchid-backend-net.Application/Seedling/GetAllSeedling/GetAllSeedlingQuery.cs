using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Application.Common.Extension;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Seedling.GetAllSeedling
{
    public class GetAllSeedlingQuery : IRequest<PageResult<SeedlingDTO>>, IQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? ByMother { get; set; }
        public string? ByFather { get; set; }
        public string? SearchTerm { get; set; }
        public bool IncludeDeleted { get; set; } = false;
        public GetAllSeedlingQuery(int pageNumber, int pageSize, string byMother, string byFather,string searchTerm)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.ByMother = byMother;
            this.ByFather = byFather;
            this.SearchTerm = searchTerm;
        }
        public GetAllSeedlingQuery()
        {
        }
    }

    internal class  GetAllSeedlingQueryHandler(ISeedlingRepository seedlingRepository) : IRequestHandler<GetAllSeedlingQuery, PageResult<SeedlingDTO>>
    {
        public async Task<PageResult<SeedlingDTO>> Handle(GetAllSeedlingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<Seedlings> queryOptions(IQueryable<Seedlings> query)
                {
                    if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                        query = query.Where(x => x.Name.Equals(request.SearchTerm));

                    if (!string.IsNullOrWhiteSpace(request.ByMother))
                        query = query.Where(x => x.Mother.ToLower().Equals(request.ByMother.ToLower()));

                    if (!string.IsNullOrWhiteSpace(request.ByFather))
                        query = query.Where(x => x.Father.ToLower().Equals(request.ByFather.ToLower()));
                    return query;
                }
                var seedlings = await seedlingRepository.FindAllProjectToAsync<SeedlingDTO>(
                    pageNo: request.PageNumber,
                    pageSize: request.PageSize,
                    queryOptions: queryOptions,
                    cancellationToken: cancellationToken);
                return seedlings.ToAppPageResult();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
