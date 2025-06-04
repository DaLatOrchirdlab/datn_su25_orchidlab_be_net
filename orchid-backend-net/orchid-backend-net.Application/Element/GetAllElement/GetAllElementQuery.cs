using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Element.GetAllElement
{
    public class GetAllElementQuery : IRequest<PageResult<ElementDTO>>, IQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllElementQuery(int pagenumber, int pagesize)
        {
            this.PageNumber = pagenumber;
            this.PageSize = pagesize;
        }
        public GetAllElementQuery() { }
    }
}
