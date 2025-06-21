using MediatR;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Element.CreateElement
{
    public class CreateElementCommandHandler(IElementRepositoty elementRepositoty) : IRequestHandler<CreateElementCommand, string>
    {
        private readonly IElementRepositoty _elementRepositoty;
        public async Task<string> Handle(CreateElementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dupChecker = await this._elementRepositoty.FindAsync(x => x.Name.Equals(request.Name) && x.Status == true, cancellationToken);
                if (dupChecker != null)
                    throw new DuplicateException($"Exit element with name : {request.Name}");
                Domain.Entities.Element obj = new Domain.Entities.Element()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Status = true,
                };
                this._elementRepositoty.Add(obj);
                return await this._elementRepositoty.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Created element name : {request.Name}." : "Failed create element.";
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
