using AutoMapper;

namespace orchid_backend_net.Application.User
{
    public static class UserMappingExtentations
    {
        public static UserDTO MapToUserDTO(this orchid_backend_net.Domain.Entities.User user, IMapper mapper)
            => mapper.Map<UserDTO>(user);
        public static List<UserDTO> MapToUserDTOList(this IEnumerable<orchid_backend_net.Domain.Entities.User> userList, IMapper mapper)
            => userList.Select(x => x.MapToUserDTO(mapper)).ToList();
    }
}
