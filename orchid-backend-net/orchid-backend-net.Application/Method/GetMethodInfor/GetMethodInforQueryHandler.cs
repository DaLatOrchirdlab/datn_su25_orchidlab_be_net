using AutoMapper;
using MediatR;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Method.GetMethodInfor
{
    public class GetMethodInforQueryHandler : IRequestHandler<GetMethodInforQuery, MethodDTO>
    {
        private readonly IMethodRepository _methodRepository;
        private readonly IMapper _mapper;
        public GetMethodInforQueryHandler(IMapper mapper, IMethodRepository methodRepository)
        {
            _mapper = mapper;
            _methodRepository = methodRepository;
        }
        public async Task<MethodDTO> Handle(GetMethodInforQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var method = await this._methodRepository.FindAsync(x => x.ID.Equals(request.ID) && x.Status == true, cancellationToken);
                if (method == null)
                    throw new NotFoundException($"Not found any method with ID : {request.ID}.");
                return method.MapToMethodDTO(_mapper);
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
