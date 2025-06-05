using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.ExperimentLog.GetExperimentLogInfor
{
    public class GetExperimentLogInforQuery : IRequest<ExperimentLogDTO>, IQuery
    {
        public string ID {  get; set; }
        public GetExperimentLogInforQuery(string iD)
        {
            ID = iD;
        }
        public GetExperimentLogInforQuery() { }
    }
}
