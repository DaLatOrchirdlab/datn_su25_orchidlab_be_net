using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Report.ExportReportPDF
{
    public class ExportReportPdfCommand(string experimentLogId) : IRequest<byte[]>, ICommand
    {
        public string ExperimentLogId { get; set; } = experimentLogId;
    }

    internal class ExportReportPdfCommandHandler(IPdfReportGenerator pdfReportGenerator, IExperimentLogRepository experimentLogRepository,
        IHybridizationRepository hybridizationRepository, ISeedlingRepository seedlingRepository,
        IMethodRepository methodRepository, ITaskRepository taskRepository,
        ISampleRepository sampleRepository, ITaskAssignRepository taskAssignRepository, 
        IUserRepository userRepository) : IRequestHandler<ExportReportPdfCommand, byte[]>
    {
        public async Task<byte[]> Handle(ExportReportPdfCommand request, CancellationToken cancellationToken)
        {
            List<Seedlings> seedlings = [];
            List<string> technicianNames = [];
            var experimentLog = await experimentLogRepository.FindAsync(x => x.ID.Equals(request.ExperimentLogId), cancellationToken);

            var hybrid = await hybridizationRepository.FindAllAsync(x => x.ExperimentLogID.Equals(request.ExperimentLogId), cancellationToken);
            foreach (var hype in hybrid)
            {
                var seedling = await seedlingRepository.FindAsync(x => x.ID.Equals(hype.ParentID), cancellationToken);
                seedlings.Add(seedling);
            }

            var method = await methodRepository.FindAsync(x => x.ID.Equals(experimentLog.MethodID), cancellationToken);
            var tasks = await taskRepository.FindAllAsync(x => x.Linkeds.Any(linkeds => linkeds.ExperimentLogID!.Equals(request.ExperimentLogId)), cancellationToken);
            var taskIds = tasks.Select(task => task.ID).ToList();
            var assigns = await taskAssignRepository.FindAllAsync(taskAssign => taskIds.Contains(taskAssign.TaskID), cancellationToken);
            foreach (var technician in assigns)
            {
                var user = await userRepository.FindAsync(x => x.ID.Equals(technician.TechnicianID), cancellationToken);
                technicianNames.Add(user.Name);
            }
            technicianNames = technicianNames.Distinct().ToList();

            var samples = await sampleRepository.FindAllAsync(x => x.Linkeds.Any(linkeds => linkeds.ExperimentLogID!.Equals(request.ExperimentLogId)), cancellationToken);

            int totalDate = 0;
            foreach (var stage in method.Stages)
            {
                totalDate += stage.DateOfProcessing;
            }

            object experimentLogData = new
            {
                experimentLog = new
                {
                    experimentLog.Name,
                    experimentLog.Create_by,
                    Create_date = experimentLog.Create_date,
                    End_date = totalDate,
                    Delete_date = experimentLog.Delete_date,
                },
                technicianNames,
                seedlings,
                method,
                task = tasks,
                samples
            };
            var pdfBytes = await pdfReportGenerator.GenerateAsync(experimentLogData);
            return pdfBytes;
        }
    }
}
