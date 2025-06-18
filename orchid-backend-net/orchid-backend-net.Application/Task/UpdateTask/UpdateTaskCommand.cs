using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Task.UpdateTask
{
    public class UpdateTaskCommand : IRequest<string>, ICommand
    {
        public string ID {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public UpdateTaskCommand(string id, string name, string description, int status)
        {
            ID = id;
            Name = name;
            Description = description;
            Status = status;
        }
    }
}
