using MediatR;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.LabRoom.UpdateLabRoom
{
    public class UpdateLabRoomCommandHandler : IRequestHandler<UpdateLabRoomCommand, string>
    {
        private readonly ILabRoomRepository _labRoomRepository;
        public UpdateLabRoomCommandHandler(ILabRoomRepository labRoomRepository)
        {
            _labRoomRepository = labRoomRepository;
        }
        public async Task<string> Handle(UpdateLabRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var labroom = await this._labRoomRepository.FindAsync(x => x.ID.Equals(request.ID));
                if (labroom == null)
                    throw new Exception($"Not found labroom with ID : {request.ID}.");
                labroom.Name = request.Name;
                labroom.Description = request.Description;
                this._labRoomRepository.Update(labroom);
                return await this._labRoomRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Updated labroom with ID :{request.ID}" : $"Failed to update labroom with ID :{request.ID}";
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
