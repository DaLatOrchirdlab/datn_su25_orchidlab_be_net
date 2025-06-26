using AutoMapper;
using MediatR;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Tasks.GetAllTasks
{
    public class GetAllTaskQueryHandler : IRequestHandler<GetAllTaskQuery, PageResult<TaskDTO>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskAssignRepository _taskAssignRepository;
        private readonly IMapper _mapper;
        public GetAllTaskQueryHandler(ITaskRepository taskRepository, IMapper mapper, ITaskAssignRepository taskAssignRepository)
        {
            _taskAssignRepository = taskAssignRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<PageResult<TaskDTO>> Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._taskRepository.FindAllAsync(x => x.Delete_by == null, request.PageNumber, request.PageSize, cancellationToken);
                if (result == null)
                    throw new NotFoundException("not found any Task in the system");
                //foreach (var task in result) 
                //{
                //    //var technicianList = await this._taskAssignRepository.FindAllAsync(x => x.TaskID.Equals(task.ID), cancellationToken);
                //    foreach (var item in await this._taskAssignRepository.FindAllAsync(x => x.TaskID.Equals(task.ID), cancellationToken)) 
                //    {

                //    }
                //}
                return PageResult<TaskDTO>.Create(
                    totalCount: result.TotalCount,
                    pageCount: result.PageCount,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize,
                    data: result.MapToTaskDTOList(_mapper)
                    );
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
