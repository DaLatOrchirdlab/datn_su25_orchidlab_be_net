using MediatR;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Method.CreateMethod
{
    public class CreateMethodCommandHandler : IRequestHandler<CreateMethodCommand, string>
    {
        private readonly IMethodRepository _methodRepository;
        public CreateMethodCommandHandler(IMethodRepository methodRepository)
        {
            _methodRepository = methodRepository;
        }
        public async Task<string> Handle(CreateMethodCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var checker = await this._methodRepository.FindAsync(x => x.Name.Equals(request.Name) && x.Status == true, cancellationToken);
                if (checker == null)
                    throw new DuplicateException($"Duplicate method name :{request.Name}");
                Domain.Entities.Method obj = new Domain.Entities.Method()
                {
                    Name = request.Name,
                    Status = true,
                    Description = request.Description,
                    Type = request.Type,
                };
                this._methodRepository.Add(obj);
                return await this._methodRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Created method : {obj.Name}" : "Failed to create method.";
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
