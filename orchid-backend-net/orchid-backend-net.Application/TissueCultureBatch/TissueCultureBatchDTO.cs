using orchid_backend_net.Application.Common.Mappings;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Application.TissueCultureBatch
{
    public class TissueCultureBatchDTO : IMapFrom<TissueCultureBatches>
    {
        public string ID { get; set; }

        public static TissueCultureBatchDTO Create(TissueCultureBatches entity)
        {
            return new TissueCultureBatchDTO
            {
                ID = entity.ID,
            };
        }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<TissueCultureBatches, TissueCultureBatchDTO>()
                .ReverseMap();
        }
    }
}
