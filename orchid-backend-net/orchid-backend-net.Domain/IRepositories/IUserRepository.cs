using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Domain.IRepositories
{
    public interface IUserRepository : IEFRepository<User, User>
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }
}
