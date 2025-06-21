using MediatR;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.LabRoom.CreateLabRoom
{
    public class CreateLabRoomCommandHandler : IRequestHandler<CreateLabRoomCommand, string>
    {
        private readonly ILabRoomRepository _labRoomRepository;
        public async Task<string> Handle(CreateLabRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var checker = await this._labRoomRepository.FindAsync(x => x.Name.Equals(request.Name), cancellationToken);
                if (checker != null)
                    throw new DuplicateException($"Extis LabRoom name :{request.Name}");
                Domain.Entities.LabRoom obj = new Domain.Entities.LabRoom()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = request.Name,
                    Description = request.Description,
                    Status = true
                };
                this._labRoomRepository.Add(obj);
                return await this._labRoomRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Created LabRoom name :{obj.Name}": "Failed create LabRoom.";
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
