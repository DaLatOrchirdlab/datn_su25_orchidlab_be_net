using AutoMapper;
using MediatR;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Report.GetReportInfor
{
    public class GetReportInforQueryHandler : IRequestHandler<GetReportInforQuery, ReportDTO>
    {
        private readonly IRepostRepository _reportRepository;
        private readonly IMapper _mapper;
        public GetReportInforQueryHandler(IMapper mapper, IRepostRepository reportRepository)
        {
            _mapper = mapper;
            _reportRepository = reportRepository;
        }
        public async Task<ReportDTO> Handle(GetReportInforQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._reportRepository.FindAsync(x => x.ID.Equals(request.ID) && x.Status == true, cancellationToken);
                if (result == null)
                    throw new NotFoundException($"Not found report with ID :{request.ID}");
                return result.MapToReportDTO(_mapper);
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
