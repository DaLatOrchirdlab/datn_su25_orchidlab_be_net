using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Disease.Analysis
{
    public class DiseaseAnalysisCommand : IRequest<AnalysisDTO>, ICommand
    {
        public required byte[] ImageBytes { get; set; }
    }

    internal class DiseaseAnalysisCommandHandler(IOrchidAnalyzerService orchidAnalyzerService, IDiseaseRepository diseaseRepository) : IRequestHandler<DiseaseAnalysisCommand, AnalysisDTO>
    {
        public async Task<AnalysisDTO> Handle(DiseaseAnalysisCommand request, CancellationToken cancellationToken)
        {
            try
            {
                OrchidAnalysisResult result = await orchidAnalyzerService.AnalyzeAsync(request.ImageBytes);
                var disease = await diseaseRepository.FindProjectToAsync<DiseaseDTO>(
                    query => query.Where(disease => disease.ID.Equals(result.Disease)), cancellationToken);
                return new AnalysisDTO(result.Stage, disease);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
