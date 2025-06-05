using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.OrchidAnalysis
{
    public class OrchidAnalyzerCommandHandler(IOrchidAnalyzerService orchidAnalysisService) : IRequestHandler<OrchidAnalyzerCommand, OrchidAnalysisResult>
    {

        public async Task<OrchidAnalysisResult> Handle(OrchidAnalyzerCommand request, CancellationToken cancellationToken)
        {
            return await orchidAnalysisService.AnalyzeAsync(request.ImageBytes);
        }
    }
}
