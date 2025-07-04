﻿using AutoMapper;
using MediatR;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Sample.GetAllSampleInExperimentLog
{
    public class GetAllSampleInExperimentLogQueryHandler : IRequestHandler<GetAllSampleInExperimentLogQuery, PageResult<SampleDTO>>
    {
        private readonly ISampleRepository _sampleRepository;
        private readonly ILinkedRepository _linkedRepository;
        private readonly IMapper _mapper;
        public GetAllSampleInExperimentLogQueryHandler(ISampleRepository sampleRepository, ILinkedRepository linkedRepository, IMapper mapper)
        {
            _sampleRepository = sampleRepository;
            _linkedRepository = linkedRepository;
            _mapper = mapper;
        }

        public async Task<PageResult<SampleDTO>> Handle(GetAllSampleInExperimentLogQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var listSample = await this._linkedRepository.FindAllAsync(x => x.ExperimentLogID.Equals(request.ExperimentLogID) && x.Status == true && x.TaskID == null, request.PageNumber, request.PageSize, cancellationToken);
                List<orchid_backend_net.Domain.Entities.Samples> result = new List<Domain.Entities.Samples>();
                foreach (var sample in listSample)
                {
                    var a = _sampleRepository.FindAsync(x => x.ID.Equals(sample.SampleID), cancellationToken);
                    if (a != null)
                        result.Add(await a);
                }
                return PageResult<SampleDTO>.Create(
                    totalCount: result.Count,
                    pageCount: (result.Count / request.PageSize),
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize,
                    data: result.MapToSampleDTOList(_mapper)
                    );
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
