using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.IRepositories;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Application.Report.CreateReport
{
    public class CreateReportCommand : IRequest<string>, ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Technician { get; set; }
        public string Sample { get; set; }
        public CreateReportCommand() { }
        public CreateReportCommand(string name, string description, string technician, string sample)
        {
            Name = name;
            Description = description;
            Technician = technician;
            Sample = sample;
        }
    }

    internal class CreateReportCommandHandler(IReportRepository repostRepository, IUserRepository userRepository, ISampleRepository sampleRepository) : IRequestHandler<CreateReportCommand, string>
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
                return await repostRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Created report." : "Failed to create report.";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
