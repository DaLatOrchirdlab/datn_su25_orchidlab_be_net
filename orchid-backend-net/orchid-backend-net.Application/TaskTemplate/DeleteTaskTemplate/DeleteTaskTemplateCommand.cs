using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.TaskTemplate.DeleteTaskTemplate
{
    public class DeleteTaskTemplateCommand : IRequest, ICommand
    {
        public string ID {  get; set; }
        public DeleteTaskTemplateCommand(string ID)
        {
            this.ID = ID;
        }
    }
    internal class DeleteTaskTemplateCommandHandler : IRequestHandler<DeleteTaskTemplateCommand>
    {
        private readonly ITaskTemplatesRepository _tasksRepository;
        public async Task Handle(DeleteTaskTemplateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var taskTemplate = await this._tasksRepository.FindAsync(x => x.ID.Equals(request.ID), cancellationToken);
                if (taskTemplate != null)
                    throw new NotFoundException($"Not found ask template with ID {request.ID}");
                taskTemplate.Status = false;
                _tasksRepository.Update(taskTemplate);
                await _tasksRepository.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
