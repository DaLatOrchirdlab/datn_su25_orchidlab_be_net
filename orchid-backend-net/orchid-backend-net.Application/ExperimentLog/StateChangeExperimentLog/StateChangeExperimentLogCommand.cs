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
        public StateChangeExperimentLogCommand() { }
        public StateChangeExperimentLogCommand(string ELID)
        {
            this.ELID = ELID;
        }
    }

    internal class StateChangeExperimentLogCommandHandler(IStageRepository stageRepository,
        ILinkedRepository linkedRepository,
        IExperimentLogRepository experimentLogRepository,
        ISampleRepository sampleRepository
        ) : IRequestHandler<StateChangeExperimentLogCommand, string>
    {

        public async Task<string> Handle(StateChangeExperimentLogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var EL = await experimentLogRepository.FindAsync(x => x.ID.Equals(request.ELID), cancellationToken);
                var stageStep = (await stageRepository.FindAsync(x => x.ID.Equals(EL.CurrentStageID), cancellationToken))?.Step;
                stageStep += 1;
                var nextStage = await stageRepository.FindAsync(x => x.MethodID.Equals(EL.MethodID) && x.Step == stageStep, cancellationToken);
                if (nextStage == null)
                    throw new NotFoundException("Not found next stage, that is the last one.");
                List<Domain.Entities.Samples> sample = await sampleRepository.FindAllAsync(x => x.Linkeds.Any(x => x.ExperimentLogID.Equals(EL.ID)), cancellationToken);
                foreach( var sampleItem in sample)
                {
                    linkedRepository.Add(new Domain.Entities.Linkeds()
                    {
                        ExperimentLogID = request.ELID,
                        ProcessStatus = 0,
                        StageID = nextStage.ID,
                        SampleID = sampleItem.ID,
                        TaskID = null
                    });
                }

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
