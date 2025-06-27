using MediatR;

namespace orchid_backend_net.Application.User.CreateUser
{
    public class CreateUserCommand : IRequest<string>
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required int RoleID { get; set; }

        public CreateUserCommand(string name, string email, string phoneNumber, int roleID)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            RoleID = roleID;
        }

        public CreateUserCommand()
        {
        }
    }
}
