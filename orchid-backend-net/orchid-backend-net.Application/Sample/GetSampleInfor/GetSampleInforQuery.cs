using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Sample.GetSampleInfor
{
    public class GetSampleInforQuery : IRequest<SampleDTO>, IQuery
    {
        public string ID {  get; set; }
        public GetSampleInforQuery(string iD)
        {
            ID = iD;
        }
        public GetSampleInforQuery() { }
    }
}
