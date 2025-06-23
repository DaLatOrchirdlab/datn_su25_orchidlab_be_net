using MediatR;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Method.DeleteMethod
{
    public class DeleteMethodCommandHandler : IRequestHandler<DeleteMethodCommand, string>
    {
        private readonly IMethodRepository _methodRepository;
        public DeleteMethodCommandHandler(IMethodRepository methodRepository)
        {
            _methodRepository = methodRepository;
        }
        public async Task<string> Handle(DeleteMethodCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var method = await this._methodRepository.FindAsync(x => x.ID.Equals(request.ID) && x.Status == true, cancellationToken);
                if (method == null)
                    throw new NotFoundException($"Not found method with ID :{request.ID}");
                method.Status = false;
                this._methodRepository.Update(method);
                return await this._methodRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Deleted method name :{method.Name}" : "Failed to delete method.";
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
