using AutoMapper;
using orchid_backend_net.Application.Sample;
using orchid_backend_net.Application.TaskAssign;
using orchid_backend_net.Application.TaskAttribute;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Task
{
    public static class TaskMappingExtentations
    {

        public static TaskDTO MapToTaskDTO(this orchid_backend_net.Domain.Entities.Task task, IMapper mapper)
            => mapper.Map<TaskDTO>(task);
        
        public static List<TaskDTO> MapToTaskDTOList(this IEnumerable<orchid_backend_net.Domain.Entities.Task> taskList, IMapper mapper)
            => taskList.Select(x => x.MapToTaskDTO(mapper)).ToList();
            
    }
}
