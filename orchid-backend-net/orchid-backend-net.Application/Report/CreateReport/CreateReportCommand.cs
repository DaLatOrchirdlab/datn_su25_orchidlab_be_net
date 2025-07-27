using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.IRepositories;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Application.ReportAttribute.CreateReportAttribute;

namespace orchid_backend_net.Application.Report.CreateReport
{
    public class CreateReportCommand(string name, string description, string technician, string sample, List<CreateReportAttributeCommand> attributeCommands) : IRequest<string>, ICommand
    {
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public string Technician { get; set; } = technician;
        public string Sample { get; set; } = sample;
        public List<CreateReportAttributeCommand> AttributeCommands { get; set; } = attributeCommands;
    }

    internal class CreateReportCommandHandler(IReportRepository repostRepository, ISampleRepository sampleRepository, ISender sender) : IRequestHandler<CreateReportCommand, string>
    {
        public async Task<string> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Reports obj = new()
                {
                    ID = Guid.NewGuid().ToString(),
                    Description = request.Description,
                    Name = request.Name,
                    SampleID = request.Sample,
                    TechnicianID = request.Technician,
                    Status = true
                };
                repostRepository.Add(obj);
                foreach (var attributeCommand in request.AttributeCommands)
                {
                    attributeCommand.ReportID = obj.ID; // Set the ReportID for each attribute
                    sender.Send(attributeCommand, cancellationToken); // Send the command to create report attributes
                }
                return await repostRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Created report." : "Failed to create report.";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
