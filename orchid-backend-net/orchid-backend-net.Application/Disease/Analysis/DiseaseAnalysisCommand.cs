using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Disease.Analysis
{
    public class DiseaseAnalysisCommand : IRequest<OrchidAnalysisResult>, ICommand
    {
        public required byte[] ImageBytes { get; set; }
    }

    internal class DiseaseAnalysisCommandHandler(IOrchidAnalyzerService orchidAnalyzerService, IDiseaseRepository diseaseRepository) : IRequestHandler<DiseaseAnalysisCommand, OrchidAnalysisResult>
    {
        public async Task<OrchidAnalysisResult> Handle(DiseaseAnalysisCommand request, CancellationToken cancellationToken)
        {
            try
            {
                OrchidAnalysisResult result = await orchidAnalyzerService.AnalyzeAsync(request.ImageBytes);
                var disease = await diseaseRepository.FindAsync(x => x.ID.Equals(result.Disease), cancellationToken);
                return new OrchidAnalysisResult
                {
                    Stage = result.Stage,
                    Disease = disease?.Name ?? "Unknown Disease"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
