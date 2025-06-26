using MediatR;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Element.DeleteElement
{
    public class DeleteElementCommandHandler(IElementRepositoty elementRepositoty) : IRequestHandler<DeleteElementCommand, string>
    {
        private readonly IElementRepositoty _elementRepository;
        public async Task<string> Handle(DeleteElementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var element = await this._elementRepository.FindAsync(x => x.ID.Equals(request.ID), cancellationToken);
                if (element == null)
                    throw new NotFoundException($" Not found element with ID: {request.ID}");
                element.Status = false;
                this._elementRepository.Update(element);
                return await this._elementRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Deleted element with ID: {request.ID}" : $"Failed delete element with ID :{request.ID}";
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
