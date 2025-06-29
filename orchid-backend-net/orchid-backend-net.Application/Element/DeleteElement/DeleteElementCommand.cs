using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Element.DeleteElement
{
    public class DeleteElementCommand : IRequest<string>, ICommand
    {
        public string ID { get; set; }
        public DeleteElementCommand() { }
        public DeleteElementCommand(string ID)
        {
            this.ID = ID;
        }
    }
    internal class DeleteElementCommandHandler(IElementRepositoty elementRepositoty) : IRequestHandler<DeleteElementCommand, string>
    {
        private readonly IElementRepositoty _elementRepository;
        public async Task<string> Handle(DeleteElementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var element = await this._elementRepository.FindAsync(x => x.ID.Equals(request.ID), cancellationToken);
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
