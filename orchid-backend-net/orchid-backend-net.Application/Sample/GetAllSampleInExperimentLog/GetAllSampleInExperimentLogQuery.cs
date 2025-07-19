using AutoMapper;
using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;

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

    internal class GetAllSampleInExperimentLogQueryHandler(ISampleRepository sampleRepository, ILinkedRepository linkedRepository, IMapper mapper) : IRequestHandler<GetAllSampleInExperimentLogQuery, PageResult<SampleDTO>>
    {
        public async Task<PageResult<SampleDTO>> Handle(GetAllSampleInExperimentLogQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var listSample = await linkedRepository.FindAllAsync(x => x.ExperimentLogID.Equals(request.ExperimentLogID) && x.ProcessStatus == 0 && x.TaskID == null, request.PageNumber, request.PageSize, cancellationToken);
                List<Samples> result = new();
                foreach (var sample in listSample)
                {
                    var a = sampleRepository.FindAsync(x => x.ID.Equals(sample.SampleID), cancellationToken);
                    if (a != null)
                        result.Add(await a);
                }
                return PageResult<SampleDTO>.Create(
                    totalCount: result.Count,
                    pageCount: (result.Count / request.PageSize),
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize,
                    data: result.MapToSampleDTOList(mapper)
                    );
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
