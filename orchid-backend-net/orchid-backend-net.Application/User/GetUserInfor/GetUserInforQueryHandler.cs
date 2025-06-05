using AutoMapper;
using MediatR;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.User.GetUserInfor
{
    public class GetUserInforQueryHandler : IRequestHandler<GetUserInforQuery, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserInforQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }
        public async Task<UserDTO> Handle(GetUserInforQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.FindAsync( x => x.ID.Equals(request.ID) && x.Status == true, cancellationToken);
                return user.MapToUserDTO(_mapper);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
