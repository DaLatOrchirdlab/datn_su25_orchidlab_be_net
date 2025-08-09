using MediatR;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.InfectedSample.GetAll
{
    public class GetAllInfectedSampleQuery(int pageNo, int pageSize, string? sampleName, string? diseaseName) : IRequest<PageResult<InfectedSampleDTO>>
    {
        public int PageNumber { get; set; } = pageNo;
        public int PageSize { get; set; } = pageSize;
        public string? SampleName { get; set; } = sampleName;
        public string? DiseaseName { get; set; } = diseaseName;
    }

    internal class GetAllInfectedSampleQueryHandler(IInfectedSampleRepository infectedSampleRepository, IDiseaseRepository diseaseRepository, ISampleRepository sampleRepository) : IRequestHandler<GetAllInfectedSampleQuery, PageResult<InfectedSampleDTO>>
    {
        public async Task<PageResult<InfectedSampleDTO>> Handle(GetAllInfectedSampleQuery request, CancellationToken cancellationToken)
        {
            try
            {

            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
