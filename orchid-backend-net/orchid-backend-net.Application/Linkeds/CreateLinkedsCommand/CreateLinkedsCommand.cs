using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Linkeds.CreateLinkedsCommand;

public class CreateaLinkedsCommand(string? sampleId, string? experimentLogId, string taskId) : IRequest, ICommand
{
    public string? SampleId { get; set; } = sampleId;
    public string? ExperimentLogId { get; set; } = experimentLogId;
    public required string TaskId { get; set; } = taskId;
}

internal class CreateLinkedsCommandHandler(ILinkedRepository linkedsRepository, IExperimentLogRepository experimentLogRepository) : IRequestHandler<CreateaLinkedsCommand>
{
    public async Task Handle(CreateaLinkedsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var ex
            var linked = new Domain.Entities.Linkeds()
            {
                SampleID = request.SampleId,
                ExperimentLogID = request.ExperimentLogId,
                TaskID = request.TaskId,
                StageID = request.StageId,
                ProcessStatus = 0,
            };
            linkedsRepository.Add(linked);
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"{ex.Message}");
        }
    }
}