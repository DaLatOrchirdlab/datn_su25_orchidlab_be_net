using AutoMapper;
using MediatR;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.User.GetAllUser
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, PageResult<UserDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        
        public GetAllUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public async Task<PageResult<UserDTO>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var list = await this._userRepository.FindAllAsync(x => x.Status == true, request.PageNumber, request.PageSize, cancellationToken); ;
                if(list.Count() == 0)
                    throw new NotFoundException("there're no user in the system.");
                return PageResult<UserDTO>.Create(totalCount: list.TotalCount,
                    pageCount: list.PageCount,
                    pageSize: list.PageSize,
                    pageNumber: list.PageNo,
                    data: list.MapToUserDTOList(_mapper)
                    );
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
