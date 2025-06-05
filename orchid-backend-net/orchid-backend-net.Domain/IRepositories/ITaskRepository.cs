using orchid_backend_net.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace orchid_backend_net.Domain.IRepositories
{
    public interface ITaskRepository : IEFRepository<Domain.Entities.Task, Domain.Entities.Task>
    {
    }
}
