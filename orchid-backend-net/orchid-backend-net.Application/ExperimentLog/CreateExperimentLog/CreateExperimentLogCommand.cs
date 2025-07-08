using MediatR;
using orchid_backend_net.Application.Common.Interfaces;

namespace orchid_backend_net.Application.ExperimentLog.CreateExperimentLog
{
    public class CreateExperimentLogCommand : IRequest<string>, ICommand
    {
        public string MethodID { get; set; }
        public string Description { get; set; }
        public string TissueCultureBatchID { get; set; }
        public List<string> Hybridization { get; set; }
        public string MotherID { get; set; }
        public CreateExperimentLogCommand(string methodID, string description, string tissueCultureBatchID, List<string> hybridization, string motherID)
        {
            MethodID = methodID;
            Description = description;
            TissueCultureBatchID = tissueCultureBatchID;
            Hybridization = hybridization;
            MotherID = motherID;
        }
    }
}
