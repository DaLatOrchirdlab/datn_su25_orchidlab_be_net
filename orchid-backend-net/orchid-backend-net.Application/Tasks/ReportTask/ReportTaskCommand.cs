using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Tasks.ReportTask
{
    public class ReportTaskCommand : IRequest<string>, ICommand
    {
        public string ID { get; set; }
        public Stream FileStream { get; set; }
        public string FileName { get; set; }
        public string ReportInformation { get; set; }
        public ReportTaskCommand() { }
        public ReportTaskCommand(Stream filestream, string fileName, string reportInformation, string ID)
        {
            this.ID = ID;
            this.FileStream = filestream;
            this.FileName = fileName;
            this.ReportInformation = reportInformation;
        }
    }
    internal class ReportTaskCommandHandler : IRequestHandler<ReportTaskCommand, string>
    {
        private readonly IImageUploaderService _uploaderService;
        private readonly ITaskRepository _taskRepository;
        public ReportTaskCommandHandler(IImageUploaderService uploaderService, ITaskRepository taskRepository)
        {
            _uploaderService = uploaderService;
            _taskRepository = taskRepository;
        }

        public async Task<string> Handle(ReportTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var task = await this._taskRepository.FindAsync(x => x.ID.Equals(request.ID), cancellationToken);
                task.Url = await _uploaderService.UpdloadImageAsync(request.FileStream, request.FileName);
                task.ReportInformation = request.ReportInformation;
                if (DateTime.UtcNow > task.End_date)
                {
                    task.Status = 4;
                }
                else
                {
                    task.Status = 3;
                }
                task.ReportInformation += "/nTask had done in: " + DateTime.UtcNow;
                this._taskRepository?.Update(task);
                return (await this._taskRepository.UnitOfWork.SaveChangesAsync(cancellationToken)) > 0 ? "Successed" : "Failed";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
