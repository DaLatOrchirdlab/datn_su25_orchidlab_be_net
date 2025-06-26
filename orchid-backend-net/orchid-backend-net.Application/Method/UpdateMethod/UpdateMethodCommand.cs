using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Method.UpdateMethod
{
    public class UpdateMethodCommand : IRequest<string>, ICommand
    {
        public string ID {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public UpdateMethodCommand(string id, string name, string description, string type)
        {
            ID = id;    
            Name = name;
            Description = description;
            Type = type;
        }
    }
}
