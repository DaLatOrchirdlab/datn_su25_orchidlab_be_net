using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.LabRoom.DeleteLabRoom
{
    public class DeleteLabRoomCommand : IRequest<string>, ICommand
    {
        public string ID {  get; set; }
        public DeleteLabRoomCommand(string ID)
        {
            this.ID = ID;
        }
    }
}
