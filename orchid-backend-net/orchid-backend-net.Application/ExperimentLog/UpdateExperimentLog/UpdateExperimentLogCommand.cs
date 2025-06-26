using MediatR;
using orchid_backend_net.Application.Common.Interfaces;

namespace orchid_backend_net.Application.ExperimentLog.UpdateExperimentLog
{
    public class UpdateExperimentLogCommand : IRequest<string>, ICommand
    {
        public string ID { get; set; }
        public string MethodID { get; set; }
        public string Description { get; set; }
        public string TissueCultureBatchID { get; set; }
        public int Status { get; set; }
        public List<string> Hybridization { get; set; }
        public UpdateExperimentLogCommand(string id, string methodID, string description, string tissueCultureBatchID, int status, List<string> hybridization)
        {
            ID = id;
            MethodID = methodID;
            Description = description;
            TissueCultureBatchID = tissueCultureBatchID;
            Status = status;
            Hybridization = hybridization;
        }
    }
}
