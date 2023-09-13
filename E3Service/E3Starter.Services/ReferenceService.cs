using AutoMapper;
using E3Starter.Contracts.Persistence;
using E3Starter.Contracts.Services;
using E3Starter.Dtos;
using E3Starter.Models;
using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Services
{
    public class ReferenceService : IReferenceService
    {
        private readonly IMapper _mapper;
        private readonly IReferenceRepository _referenceRepository;

        public ReferenceService(IMapper mapper, IReferenceRepository referenceRepository)
        {
            _mapper = mapper;
            _referenceRepository = referenceRepository;
        }

      
        public async Task<List<RoleDto>> GetRolesAsync()
        {
            var roles = await _referenceRepository.GetRolesAsync();
            return _mapper.Map<List<RoleDto>>(roles);
        }

        public async Task<List<TaskDto>> GetTaskList(bool completedAt)
        {
            var mappedList = new List<TaskDto>();
            var results = await _referenceRepository.GetAllTasks(completedAt);
            foreach ( var result in results)
            {
                var priorityModel = await _referenceRepository.GetAsync<Priority>(result.PriorityId);
                var priorityDto = new PriorityDto()
                {
                    Name = priorityModel.Name,
                    Sequence = priorityModel.Sequence
                };
                var dto = new TaskDto()
                {
                    Id = result.Id,
                    TaskName = result.TaskName,
                    DeactivatedAt = result.DeactivatedAt,
                    CompletedAt = result.CompletedAt,
                    CreatedAt = result.CreatedAt,
                    DeletedAt = result.DeletedAt,
                    PriorityId = result.PriorityId,
                    Priority = priorityDto 
                };
                mappedList.Add(dto);
            }
            return mappedList;
        }

        public async Task SaveTask(TaskDto newTask) {
            var model = new Tasks()
            {
                TaskName = newTask.TaskName,
                CreatedAt = newTask.CreatedAt,
                PriorityId = newTask.PriorityId
            };
            await _referenceRepository.SaveAsync(model);
        }

        public async Task<List<PriorityDto>> GetPriorityList()
        {
            var mappedList = new List<PriorityDto>();
            var results = await _referenceRepository.GetPriorityList();
            foreach (var result in results)
            {
                //var priorityModel = await _referenceRepository.GetAsync<Priority>(result.Id);
                var thepriorityDto = new PriorityDto()
                {
                    Name = result.Name,
                    Sequence = result.Sequence
                };
                mappedList.Add(thepriorityDto);
            }
            return mappedList;
        }

        public async Task ToggleCompleted(TaskDto completedTask) 
        => await _referenceRepository.ToggleTaskComplete(completedTask);
        
        public async Task DeactivateTask(TaskDto deactivatedTask) 
        => await _referenceRepository.DeactivateTask(deactivatedTask);


        //public Task GetPriority(TaskDto priority)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<List<TaskDto>> GetTaskList()
        //{

        //        var allTasks = await _referenceRepository.GetAllTasks(false);
        //        return _mapper.Map<List<TaskDto>>(allTasks);

        //}

    }
}
