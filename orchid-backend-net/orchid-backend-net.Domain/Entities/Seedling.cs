using orchid_backend_net.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Domain.Entities
{
    public class Seedling : Entity
    {
        [Key]
        public string ID {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Parent {  get; set; }
        public string Parent1 { get; set; }
        public DateOnly Dob {  get; set; }
        public DateTime Create_at {  get; set; }
        public DateTime Update_at { get; set; }
        public string Create_by {  get; set; }
        public string Update_by { get; set; }
        public string Delete_by { get; set; }
        public DateTime Delete_at { get; set; }
    }
}
