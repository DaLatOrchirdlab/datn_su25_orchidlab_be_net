using orchid_backend_net.Domain.Entities.Base;

namespace orchid_backend_net.Domain.Entities
{
    public class Tasks : BaseSoftDelete
    {
        public string Researcher { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public DateTime Create_at { get; set; }
        public int Status {  get; set; }
        //public enum Status 
        public int Status { get; set; } // 0:  Assign,
        //1: Taken,
        //2: InProcess,
        //3: DoneInTime,
        //4: DoneInLate,
        //5: Cancel,

    }
}
