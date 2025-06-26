using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.LabRoom.UpdateLabRoom
{
    public class UpdateLabRoomCommand : IRequest<string>, ICommand
    {
        public string ID {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public UpdateLabRoomCommand(string ID, string Name, string Description)
        {
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
        }
    }
}
