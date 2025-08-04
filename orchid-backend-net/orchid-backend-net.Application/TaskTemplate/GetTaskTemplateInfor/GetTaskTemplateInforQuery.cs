using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.TaskTemplate.GetTaskTemplateInfor
{
    public class GetTaskTemplateInforQuery : IRequest<TaskTemplateDTO>, IQuery
    {
        public string ID {  get; set; }
        public GetTaskTemplateInforQuery(string ID)
        {
            this.ID = ID;
        }
    }
    internal class GetTaskTemplateInforQueryHandler : IRequestHandler<GetTaskTemplateInforQuery, TaskTemplateDTO>
    {
        public readonly ITaskTemplatesRepository _taskTemplateRepository;
        public GetTaskTemplateInforQueryHandler(ITaskTemplatesRepository taskTemplateRepository)
        {
            _taskTemplateRepository = taskTemplateRepository;
        }
        public async Task<TaskTemplateDTO> Handle(GetTaskTemplateInforQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var obj = await this._taskTemplateRepository.FindProjectToAsync<TaskTemplateDTO>(query => query.Where(x => x.ID.Equals(request.ID)), cancellationToken : cancellationToken);
                return obj == null ? obj : throw new NotFoundException($"not found task template with ID {request.ID}");
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
