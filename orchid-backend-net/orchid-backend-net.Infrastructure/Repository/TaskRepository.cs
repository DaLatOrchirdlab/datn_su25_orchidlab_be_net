using AutoMapper;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;
using orchid_backend_net.Infrastructure.Persistence;

namespace orchid_backend_net.Infrastructure.Repository
{
    public class TaskRepository(OrchidDbContext context, IMapper mapper) : RepositoryBase<Domain.Entities.Task, Domain.Entities.Task, OrchidDbContext>(context, mapper), ITaskRepository
    {
    }
}
