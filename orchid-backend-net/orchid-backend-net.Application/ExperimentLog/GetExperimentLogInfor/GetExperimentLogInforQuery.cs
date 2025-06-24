using MediatR;
using orchid_backend_net.Application.Common.Interfaces;

namespace orchid_backend_net.Application.ExperimentLog.GetExperimentLogInfor
{
    public class GetExperimentLogInforQuery : IRequest<ExperimentLogDTO>, IQuery
    {
        public required string ID { get; set; }
        public GetExperimentLogInforQuery(string id)
        {
            ID = id;
        }
        public GetExperimentLogInforQuery() { }
    }
}
