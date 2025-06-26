using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Element.UpdateElement
{
    public class UpdateElementCommand : IRequest<string>, ICommand
    {
        public string ID {  get; set; }
        public string Name {  get; set; }
        public string Description { get; set; }
        public UpdateElementCommand(string id, string name, string description)
        {
            ID = id;
            Name = name;
            Description = description;
        }
        public UpdateElementCommand() { }
    }
}
