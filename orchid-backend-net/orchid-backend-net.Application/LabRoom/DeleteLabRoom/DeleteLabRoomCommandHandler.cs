using MediatR;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.LabRoom.DeleteLabRoom
{
    public class DeleteLabRoomCommandHandler : IRequestHandler<DeleteLabRoomCommand, string>
    {
        private readonly ILabRoomRepository _labRoomRepository;
        public DeleteLabRoomCommandHandler(ILabRoomRepository labRoomRepository)
        {
            _labRoomRepository = labRoomRepository;
        }

        public async Task<string> Handle(DeleteLabRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var labroom = await this._labRoomRepository.FindAsync(x => x.ID.Equals(request.ID) && x.Status == true , cancellationToken);
                if (labroom == null)
                    throw new NotFoundException($"Not found LabRoom with ID : {request.ID}.");
                labroom.Status= false;
                this._labRoomRepository.Update(labroom);
                return await this._labRoomRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Deleted LabRoom with ID :{request.ID}" : $"Failed to delete LabRoom with ID : {request.ID}";
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
