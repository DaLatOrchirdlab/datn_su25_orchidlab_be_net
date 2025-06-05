using AutoMapper;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;
using orchid_backend_net.Infrastructure.Persistence;

namespace orchid_backend_net.Infrastructure.Repository
{
    public class ElementRepository(OrchidServerDbContext context, IMapper mapper) : RepositoryBase<Element, Element, OrchidServerDbContext>(context, mapper), IElementRepositoty
    {
    }
}
