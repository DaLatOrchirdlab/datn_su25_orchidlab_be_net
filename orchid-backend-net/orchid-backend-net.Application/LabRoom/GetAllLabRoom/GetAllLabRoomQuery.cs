using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.LabRoom.GetAllLabRoom
{
    public class GetAllLabRoomQuery : IRequest<PageResult<LabRoomDTO>>, IQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllLabRoomQuery() { }
        public GetAllLabRoomQuery(int pageNumber, int pageSize) 
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
