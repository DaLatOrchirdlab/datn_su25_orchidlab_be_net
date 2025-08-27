using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.IRepositories;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Application.ReportAttribute.CreateReportAttribute;

namespace orchid_backend_net.Application.Report.CreateReport
{
    public class CreateReportCommand(string name, string description, string sample, List<CreateReportAttributeCommand> attributeCommands) : IRequest<string>, ICommand
    {
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public string Sample { get; set; } = sample;
        public List<CreateReportAttributeCommand> AttributeCommands { get; set; } = attributeCommands;
    }

    internal class CreateReportCommandHandler(IReportRepository reportRepository, ISender sender, ICurrentUserService currentUserService) : IRequestHandler<CreateReportCommand, string>
    {
        public async Task<string> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var oldReports = await reportRepository.FindAsync(x => x.IsLatest && x.SampleID.Equals(request.Sample), cancellationToken);
                if(oldReports != null)
                {
                    oldReports.IsLatest = false; // Mark the old report as not the latest
                    reportRepository.Update(oldReports); // Update the old report
                }
                Reports obj = new()
                {
                    ID = Guid.NewGuid().ToString(),
                    Description = request.Description,
                    Name = request.Name,
                    SampleID = request.Sample,
                    TechnicianID = currentUserService.UserId,
                    IsLatest = true, 
                    Status = 0,
                    Create_by = currentUserService.UserId, 
                    Create_date = DateTime.UtcNow
                };
                reportRepository.Add(obj);
                foreach (var attributeCommand in request.AttributeCommands)
                {
                    attributeCommand.ReportID = obj.ID;
                    sender.Send(attributeCommand, cancellationToken); // Send the command to create report attributes
                }
                return await reportRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"{obj.ID}" : "Failed to create report.";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
