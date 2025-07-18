using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Sample.DeleteSample
{
    public class DeleteSampleCommand(string id, string reason, string diseaseName) : IRequest, ICommand
    {
        public string Id { get; set; } = id;
        /// <summary>
        /// In this string has 2 kind of message
        /// 1. Chưa đạt yêu cầu XXXXX, with XXXXX is the referent value
        /// 2. Nhiễm bệnh XXXXX, with XXXXX is the disease name
        /// </summary>
        public string Reason { get; set; } = reason;
        public string DiseaseName { get; set; } = diseaseName;
    }

    internal class DeleteSampleCommandHandler(ISampleRepository sampleRepository, ILinkedRepository linkedRepository, IDiseaseRepository diseaseRepository) : IRequestHandler<DeleteSampleCommand>
    {
        public async Task Handle(DeleteSampleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sample = await sampleRepository.FindAsync(x => x.ID.Equals(request.Id));
                if (request.Reason.Contains("Chưa đạt yêu cầu"))
                    sample.Status = 1;
                if(request.Reason.Contains("Nhiễm bệnh"))
                {
                    var disease = await diseaseRepository.FindAsync(x => x.Name.Equals(request.DiseaseName), cancellationToken);
                    if(disease.)
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
