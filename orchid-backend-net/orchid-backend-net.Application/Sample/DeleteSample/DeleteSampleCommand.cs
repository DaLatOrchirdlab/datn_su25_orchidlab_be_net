using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Sample.DeleteSample
{
    public class DeleteSampleCommand(string id, string reason,
        string? diseaseName, int? infectedLevel,
        int? treatmentStatus) : IRequest<string>, ICommand
    {
        public string Id { get; set; } = id;
        /// <summary>
        /// In this string has 2 kind of message
        /// 1. Chưa đạt yêu cầu XXXXX, pause
        /// 2. Nhiễm bệnh => infected sample
        /// </summary>
        public string Reason { get; set; } = reason;
        public string? DiseaseName { get; set; } = diseaseName;
        public int? InfectedLevel { get; set; } = infectedLevel;
        public int? TreatmentStatus { get; set; } = treatmentStatus;
    }

    internal class DeleteSampleCommandHandler(ISampleRepository sampleRepository, ILinkedRepository linkedRepository,
        IInfectedSampleRepository infectedSampleRepository, IDiseaseRepository diseaseRepository,
        IExperimentLogRepository experimentLogRepository) : IRequestHandler<DeleteSampleCommand, string>
    {
        public async Task<string> Handle(DeleteSampleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sample = await sampleRepository.FindAsync(x => x.ID.Equals(request.Id));
                if (request.Reason.Contains("Chưa đạt yêu cầu"))
                {
                    sample.Status = 1;
                    sample.Reason = request.Reason;
                }
                if (request.Reason.Contains("Nhiễm bệnh"))
                {
                    //logic chung
                    sample.Status = 1;
                    sample.Reason = request.Reason + " " + request.DiseaseName;
                    var disease = await diseaseRepository.FindAsync(x => x.Name.Equals(request.DiseaseName), cancellationToken);
                    InfectedSamples infectedSample = new()
                    {
                        SampleID = request.Id,
                        DiseaseID = disease.ID,
                        InfectedLevel = (int)request.InfectedLevel,
                    };

                    //cây bệnh nặng điên => đem đi tiêu hủy
                    if (request.InfectedLevel == 3)
                        //change to abandoned
                        infectedSample.TreatmentStatus = 4;
                    else
                        infectedSample.TreatmentStatus = (int)request.TreatmentStatus;

                    //update in linked to pause the process
                    var linked = await linkedRepository.FindAsync(x => x.SampleID.Equals(request.Id), cancellationToken);
                    linked.Status = false;
                    linkedRepository.Update(linked);

                    //re-calculated infected rate in experiment log
                    var experimentLog = await experimentLogRepository.FindAsync(x => x.ID.Equals(linked.ExperimentLogID), cancellationToken);
                    var allSamples = await linkedRepository.FindAllAsync(x => x.ExperimentLogID.Equals(experimentLog.ID), cancellationToken);   
                    experimentLog.InfectedRateInReality = 1 / allSamples.Count + experimentLog.InfectedRateInReality;

                    experimentLogRepository.Update(experimentLog);

                    infectedSampleRepository.Add(infectedSample);

                    return await sampleRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Remove sample with id: {request.Id} succeed" : "Failed to Remove";
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
