using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.ExperimentLog.StateChangeExperimentLog
{
    public class StateChangeExperimentLogCommand : IRequest<string>, ICommand
    {
        public string ELID {  get; set; }
        public string stageID { get; set; }
        public string methodID { get; set; }
        public StateChangeExperimentLogCommand() { }
        public StateChangeExperimentLogCommand(string eLID, string stageID, string methodID)
        {
            ELID = eLID;
            this.stageID = stageID;
            this.methodID = methodID;
        }
    }

    internal class StateChangeExperimentLogCommandHandler(IStageRepository stageRepository,
        IMethodRepository methodRepository,
        IExperimentLogRepository experimentLogRepository
        ) : IRequestHandler<StateChangeExperimentLogCommand, string>
    {

        public async Task<string> Handle(StateChangeExperimentLogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var stageStep = (await stageRepository.FindAsync(x => x.ID.Equals(request.stageID), cancellationToken))?.Step;
                stageStep += 1;
                var nextStage = await stageRepository.FindAsync(x => x.MethodID.Equals(request.methodID) && x.Step == stageStep, cancellationToken);
                if (nextStage == null)
                    throw new NotFoundException("Not found next stage, that is the last one.");
                var EL = await experimentLogRepository.FindAsync(x => x.ID.Equals(request.ELID), cancellationToken);
                EL.CurrentStageID = nextStage.ID;
                experimentLogRepository.Update(EL);
                return await experimentLogRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Successed" : "Failed";
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
