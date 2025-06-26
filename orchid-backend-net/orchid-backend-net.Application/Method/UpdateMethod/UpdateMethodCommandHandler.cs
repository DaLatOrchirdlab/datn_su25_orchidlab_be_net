using MediatR;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Method.UpdateMethod
{
    public class UpdateMethodCommandHandler : IRequestHandler<UpdateMethodCommand, string>
    {
        private readonly IMethodRepository _methodRepository;
        public UpdateMethodCommandHandler(IMethodRepository methodRepository)
        {
            _methodRepository = methodRepository;
        }
        public async Task<string> Handle(UpdateMethodCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var method = await this._methodRepository.FindAsync(x => x.ID.Equals(request.ID) && x.Status == true, cancellationToken);
                if (method == null)
                    throw new NotFoundException($"Not found any method with ID : {request.ID}.");
                method.Name = request.Name;
                method.Description = request.Description;
                method.Type = request.Type;
                this._methodRepository.Update(method);
                return await this._methodRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Updated method name :{method.Name}" : "Failed to update method.";
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
