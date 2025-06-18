using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace orchid_backend_net.Domain.Entities
{
    public class Task : BaseAutotable
    {
        [Key]
        public string ID {  get; set; }
        public string Researcher { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public DateTime Create_at { get; set; }
        public int Status {  get; set; }
        //public enum Status 

    }
}
