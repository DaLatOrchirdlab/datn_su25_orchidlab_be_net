﻿using AutoMapper;
using MediatR;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Tasks.GetTaskInfor
{
    public class GetTaskInforQueryHandler : IRequestHandler<GetTaskInforQuery, TaskDTO>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        public async Task<TaskDTO> Handle(GetTaskInforQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return (await _taskRepository.FindAsync(x => x.ID.Equals(request.ID), cancellationToken)).MapToTaskDTO(_mapper);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
