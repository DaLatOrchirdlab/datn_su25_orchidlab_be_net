using AutoMapper;
using MediatR;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Sample.GetSampleInfor
{
    public class GetSampleInforQueryHandler : IRequestHandler<GetSampleInforQuery, SampleDTO>
    {
        private readonly ISampleRepository _sampleRepository;
        private readonly IMapper _mapper;
        public GetSampleInforQueryHandler(ISampleRepository sampleRepository, IMapper mapper)
        {
            _sampleRepository = sampleRepository;
            _mapper = mapper;
        }

        public async Task<SampleDTO> Handle(GetSampleInforQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._sampleRepository.FindAsync(x => x.ID.Equals(request.ID), cancellationToken);
                if (result == null)
                    throw new NotFoundException($"Not found sample with ID:{request.ID} in the system.");
                return result.MapToSampleDTO(_mapper);
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
