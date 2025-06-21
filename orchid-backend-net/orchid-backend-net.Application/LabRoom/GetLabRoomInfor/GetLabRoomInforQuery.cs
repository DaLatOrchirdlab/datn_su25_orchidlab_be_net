using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.LabRoom.GetLabRoomInfor
{
    public class GetLabRoomInforQuery : IRequest<LabRoomDTO>, IQuery
    {
        public string ID {  get; set; }
        public GetLabRoomInforQuery(string iD)
        {
            ID = iD;
        }
    }
}
