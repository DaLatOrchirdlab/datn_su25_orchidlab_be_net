using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Domain.Entities.Base
{
    public class BaseEntity
    {
        protected BaseEntity()
        {
            ID = Guid.NewGuid().ToString("N");
            NgayTao = NgayCapNhatCuoi = DateTime.Now;
        }

        public string? NguoiTaoID { get; set; }
        public DateTime? NgayTao { get; set; }

        public string? NguoiCapNhatID { get; set; }
        public DateTime? NgayCapNhatCuoi { get; set; }

        public string? NguoiXoaID { get; set; }
        public DateTime? NgayXoa { get; set; }


        [Key]
        public string ID { get; set; }
    }
}
