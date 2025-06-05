using AutoMapper;

namespace orchid_backend_net.Application.Element
{
    public static class ElementMappingExtentations
    {
        public static ElementDTO MapToElementDTO(this orchid_backend_net.Domain.Entities.Element element, IMapper mapper)
            => mapper.Map<ElementDTO>(element);
        public static List<ElementDTO> MapToElementDTOList(this IEnumerable<orchid_backend_net.Domain.Entities.Element> elementList, IMapper mapper)
            => elementList.Select(x => x.MapToElementDTO(mapper)).ToList();
    }
}
