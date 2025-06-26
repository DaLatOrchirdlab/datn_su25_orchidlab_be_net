using MediatR;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Element.UpdateElement
{
    public class UpdateElementCommandHandler : IRequestHandler<UpdateElementCommand, string>
    {
        private readonly IElementRepositoty _elementRepositoty;
        public UpdateElementCommandHandler(IElementRepositoty elementRepositoty)
        {
            _elementRepositoty = elementRepositoty;
        }

        public async Task<string> Handle(UpdateElementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var element = await this._elementRepositoty.FindAsync(x => x.ID.Equals(request.ID) && x.Status == true, cancellationToken);
                if (element == null)
                    throw new NotFoundException($"Not found any element with ID :{request.ID}");
                element.Description = request.Description;
                element.Name = request.Name;
                this._elementRepositoty.Update(element);
                return await this._elementRepositoty.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Updated element name {request.Name}" : "Failed update element";
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
