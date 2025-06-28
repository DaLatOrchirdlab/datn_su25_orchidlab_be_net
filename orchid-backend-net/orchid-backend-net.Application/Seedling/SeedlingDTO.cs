using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Application.Seedling
{
    public class SeedlingDTO : IMapFrom<Seedlings>
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? MotherID { get; set; }
        public string? FatherID { get; set; }
        public DateTime DoB { get; set; }
        public List<CharacteristicsDTO> Characteristics { get; set; } = [];
        public DateTime Create_by { get; set; }
        public DateTime Create_date { get; set; }
        public DateTime? Update_by { get; set; }
        public DateTime? Update_date { get; set; }
        public DateTime? Delete_by { get; set; }
        public DateTime? Delete_date { get; set; }

        public static SeedlingDTO Create(string id, string name, string description, 
            string? motherId, string? fatherId, DateTime doB, 
            List<CharacteristicsDTO> characteristics, DateTime createBy, DateTime createDate, 
            DateTime? updateBy = null, DateTime? updateDate = null, 
            DateTime? deleteBy = null, DateTime? deleteDate = null)
        {
            return new SeedlingDTO
            {
                ID = id,
                Name = name,
                Description = description,
                MotherID = motherId,
                FatherID = fatherId,
                DoB = doB,
                Characteristics = characteristics,
                Create_by = createBy,
                Create_date = createDate,
                Update_by = updateBy,
                Update_date = updateDate,
                Delete_by = deleteBy,
                Delete_date = deleteDate
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Seedlings, SeedlingDTO>()
                .ReverseMap();
        }
    }
}
