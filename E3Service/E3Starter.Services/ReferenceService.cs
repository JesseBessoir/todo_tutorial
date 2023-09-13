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

        public async Task<List<CategoriesDto>> GetCategoryList(TaskSearchCriteriaDto criteria)
        {
            var mappedList = new List<CategoriesDto>();
            var results = await _referenceRepository.GetAllCategories(criteria);
            foreach (var result in results)
            {
                var dto = new CategoriesDto()
                {
                    Id = result.Id,
                    CategoryName = result.CategoryName,
                    DeactivatedAt = result.DeactivatedAt,
                    CreatedAt = result.CreatedAt,
                };
                mappedList.Add(dto);
            }
            return mappedList;


        }

        public async Task<List<TaskDto>> GetTaskList(TaskSearchCriteriaDto criteria)
        {
            var mappedList = new List<TaskDto>();
            var results = await _referenceRepository.GetAllTasks(criteria);
            foreach ( var result in results)
            {
                var dto = new TaskDto()
                {
                    Id = result.Id,
                    TaskName = result.TaskName,
                    DeactivatedAt = result.DeactivatedAt,
                    CompletedAt = result.CompletedAt,
                    CreatedAt = result.CreatedAt,
                    DeletedAt = result.DeletedAt
                };
                mappedList.Add(dto);
            }
            return mappedList;


        }

        //public async Task<List<TaskCategoriesDto>> GetCategoryList()
        //{

        //}

        public async Task SaveTask(TaskDto newTask) {

            var model = new Tasks()
            {
                TaskName = newTask.TaskName,
                CreatedAt = newTask.CreatedAt
               
            };
            
            model = await _referenceRepository.CreateAsync(model);

            foreach (var c in newTask.Categories)
            {
                var newTaskCategory = new TaskCategories() 
                { 
                    CategoryId = c.Id,
                    TaskId = model.Id
                };
                await _referenceRepository.CreateAsync(newTaskCategory);
            }
        }

        public async Task ToggleCompleted(TaskDto completedTask) 
        => await _referenceRepository.ToggleTaskComplete(completedTask);
        
        public async Task DeactivateTask(TaskDto deactivatedTask) 
        => await _referenceRepository.DeactivateTask(deactivatedTask);
        



        //public async Task<List<TaskDto>> GetTaskList()
        //{

        //        var allTasks = await _referenceRepository.GetAllTasks(false);
        //        return _mapper.Map<List<TaskDto>>(allTasks);
            
        //}

    }
}
