using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.User.UpdateUser
{
    public class UpdateUserInformationCommand : IRequest<string>, ICommand
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public int? RoleId { get; set; }
    }

    public class UpdateUserInformationCommandHandler(IUserRepository userRepository, ICurrentUserService currentUserService) : IRequestHandler<UpdateUserInformationCommand, string>
    {
        public async Task<string> Handle(UpdateUserInformationCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindAsync(x => x.ID.Equals(request.Id), cancellationToken);
            user.Name = request.Name ?? user.Name;
            user.Password = request.Password ?? user.Password;
            user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
            user.RoleID = request.RoleId ?? user.RoleID;
            user.Update_date = DateTime.UtcNow;
            user.Update_by = currentUserService.UserName;
            userRepository.Update(user);
            return await userRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0
                ? "Update user successfully"
                : "Update user failed";
        }
    }
}
