using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Element.DeleteElement
{
    public class DeleteElementCommand : IRequest<string>, ICommand
    {
        public string ID {  get; set; }
        public DeleteElementCommand() { }
        public DeleteElementCommand(string ID)
        {
            this.ID = ID;
        }
    }
}
