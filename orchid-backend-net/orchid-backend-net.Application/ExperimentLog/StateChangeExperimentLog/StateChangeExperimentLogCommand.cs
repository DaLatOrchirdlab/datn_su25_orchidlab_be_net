using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.ExperimentLog.StateChangeExperimentLog
{
    public class StateChangeExperimentLogCommand : IRequest<string>, ICommand
    {
        public string ELID { get; set; }
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
                if (nextStage == null){
                    EL.Status = 1;
                    List<Domain.Entities.Linkeds> existingLinked = await linkedRepository.FindAllAsync(x => x.ExperimentLogID.Equals(EL.ID) && x.StageID.Equals(EL.CurrentStageID), cancellationToken);
                    foreach (var linked in existingLinked) 
                    {
                        linked.ProcessStatus = 2;
                        linkedRepository.Update(linked);
                    }
                }
                else
                {
                    List<Domain.Entities.Samples> sample = await sampleRepository.FindAllAsync(x => x.Linkeds.Any(x => x.ExperimentLogID.Equals(EL.ID)), cancellationToken);
                    List<Domain.Entities.Linkeds> existingLinkeds = await linkedRepository.FindAllAsync(x => x.ExperimentLogID.Equals(EL.ID) && x.StageID.Equals(EL.CurrentStageID), cancellationToken);
                    foreach (var item in existingLinkeds)
                    {
                        item.ProcessStatus = 1;
                        linkedRepository.Update(item);
                    }
                    foreach (var sampleItem in sample)
                    {
                        linkedRepository.Add(new Domain.Entities.Linkeds()
                        {
                            ExperimentLogID = request.ELID,
                            ProcessStatus = 0,
                            StageID = nextStage.ID,
                            SampleID = sampleItem.ID,
                        });
                    }
                    EL.CurrentStageID = nextStage.ID;
                }
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
