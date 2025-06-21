using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Method.CreateMethod
{
    public class CreateMethodCommand : IRequest<string>, ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public CreateMethodCommand(string name, string description, string type)
        {
            Name = name;
            Description = description;
            Type = type;
        }
    }
}
