using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Application.OrchidAnalysis
{
    public class OrchidAnalyzerCommand : IRequest<OrchidAnalysisResult>, ICommand
    {
        public required byte[] ImageBytes { get; set; }
    }
}
