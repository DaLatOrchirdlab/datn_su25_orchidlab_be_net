using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.LabRoom.CreateLabRoom
{
    public class CreateLabRoomCommand : IRequest<string>, ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CreateLabRoomCommand(string name, string description)
        {
            Name = name; 
            Description = description;
        }
    }
}
