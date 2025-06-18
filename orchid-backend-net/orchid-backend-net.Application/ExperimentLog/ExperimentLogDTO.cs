using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;
using orchid_backend_net.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.ExperimentLog
{
    public class ExperimentLogDTO : IMapFrom<orchid_backend_net.Domain.Entities.ExperimentLog>
    {
        public string MethodID { get; set; }
        public string Description { get; set; }
        public string TissueCultureBatchID { get; set; }

        public static ExperimentLogDTO Create(string methodid, string tissueculturebatchid, string description)
        {
            return new ExperimentLogDTO
            {
                MethodID = methodid,
                Description = description,
                TissueCultureBatchID = tissueculturebatchid
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<orchid_backend_net.Domain.Entities.ExperimentLog, ExperimentLogDTO>();
        }
    }
}
