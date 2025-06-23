using MediatR;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Report.CreateReport
{
    public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, string>
    {
        private readonly IRepostRepository _reportRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISampleRepository _sampleRepository;
        public CreateReportCommandHandler(IRepostRepository repostRepository, IUserRepository userRepository, ISampleRepository sampleRepository)
        {
            _reportRepository = repostRepository;
            _userRepository = userRepository;
            _sampleRepository = sampleRepository;
        }
        public async Task<string> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await this._userRepository.FindAsync(x => x.ID.Equals(request.Technician), cancellationToken) == null)
                    throw new NotFoundException("Teachnician not found in the system.");
                if (await this._sampleRepository.FindAsync(x => x.ID.Equals(request.Sample), cancellationToken) == null)
                    throw new NotFoundException("Sample not found in the system.");
                Domain.Entities.Report obj = new Domain.Entities.Report()
                {
                    ID = Guid.NewGuid().ToString(),
                    Description = request.Description,
                    Name = request.Name,
                    Sample = request.Sample,
                    Technician = request.Technician,
                    Status = true
                };
                this._reportRepository.Add(obj);
                return await this._reportRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Created report." : "Failed to create report.";
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
