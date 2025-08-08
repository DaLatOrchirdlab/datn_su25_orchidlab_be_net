using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.TissueCultureBatch.UpdateTissueCultureBatch
{
    public class UpdateTissueCultureBatchCommand : IRequest<string>, ICommand
    {
        public string ID { get; set; }
        public string? Name { get; set; }
        public string? LabName { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public UpdateTissueCultureBatchCommand(string id, string name, string labname, bool status, string description)
        {
            ID = id;
            Name = name;
            LabName = labname;
            Status = status;
            Description = description;
        }
    }

    internal class UpdateTissueCultureBatchCommandHandler : IRequestHandler<UpdateTissueCultureBatchCommand, string>
    {
        private readonly ITissueCultureBatchRepository _tissueCultureBatchRepository;
        private readonly ILabRoomRepository _labRoomRepository;
        public UpdateTissueCultureBatchCommandHandler(ITissueCultureBatchRepository tissueCultureBatchRepository, ILabRoomRepository labRoomRepository)
        {
            _tissueCultureBatchRepository = tissueCultureBatchRepository;
            _labRoomRepository = labRoomRepository;
        }
        public async Task<string> Handle(UpdateTissueCultureBatchCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tissue = await this._tissueCultureBatchRepository.FindAsync(x => x.ID.Equals(request.ID), cancellationToken);
                if (tissue == null)
                    throw new NotFoundException($"not found tissue culture batch with ID {request.ID}");
                tissue.Description = request.Description;
                tissue.Status = request.Status;
                tissue.Name = request.Name;
                if ((await this._labRoomRepository.FindAsync(x => x.ID.Equals(request.LabName), cancellationToken)) == null)
                    throw new NotFoundException($"not found lab room with ID {request.LabName}");
                return (await this._tissueCultureBatchRepository.UnitOfWork.SaveChangesAsync(cancellationToken)) > 0 ? $"Updated tissue culture batch with ID {request.ID}" : "Failed to update tissue culture batch";
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
