using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Sample.DeleteSample
{
    public class DeleteSampleCommand(string id, string reason, string diseaseName, int infectedLevel) : IRequest, ICommand
    {
        public string Id { get; set; } = id;
        /// <summary>
        /// In this string has 2 kind of message
        /// 1. Chưa đạt yêu cầu XXXXX, pause
        /// 2. Nhiễm bệnh => infected sample
        /// </summary>
        public string Reason { get; set; } = reason;
        public string DiseaseName { get; set; } = diseaseName;
        public int InfectedLevel { get; set; } = infectedLevel;
    }

    internal class DeleteSampleCommandHandler(ISampleRepository sampleRepository, ILinkedRepository linkedRepository) : IRequestHandler<DeleteSampleCommand>
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

                }    
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
