using AutoMapper;
using MediatR;
using orchid_backend_net.Application.Common.Extension;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Report.GetReportPerTech
{
    public class GetReportPerTechQuery : IRequest<PageResult<ReportDTO>>, IQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string techID {  get; set; }
        public GetReportPerTechQuery() { }
        public GetReportPerTechQuery(int pageNumber, int pageSize, string techid)
        {
            techID = techid;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
    public class GetReportPerTechQueryHandler : IRequestHandler<GetReportPerTechQuery, PageResult<ReportDTO>>
    {
        private readonly IReportRepository _repository;
        private readonly IMapper _mapper;
        public GetReportPerTechQueryHandler(IReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PageResult<ReportDTO>> Handle(GetReportPerTechQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<Reports> queryOptions(IQueryable<Reports> query)
                {
                    query = query.Where(x => x.TechnicianID.ToLower().Equals(request.techID.ToLower()));
                    return query;
                }
                var result = await _repository.FindAllProjectToAsync<ReportDTO>(
                    request.PageNumber,
                    request.PageSize,
                    queryOptions,
                    cancellationToken: cancellationToken
                    );
                return result.ToAppPageResult();
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
