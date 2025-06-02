using orchid_backend_net.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Domain.Repositories
{
    public interface IUserRepository : IEFRepository<User, User>
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }
}
