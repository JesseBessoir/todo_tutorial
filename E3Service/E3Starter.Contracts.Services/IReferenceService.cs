using E3Starter.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Contracts.Services
{
    public interface IReferenceService
    {
        Task<List<RoleDto>> GetRolesAsync();
        Task<List<CategoriesDto>> GetCategoryList(TaskSearchCriteriaDto criteria);
        Task<List<TaskDto>> GetTaskList(TaskSearchCriteriaDto criterial);
        Task SaveTask(TaskDto newTask);
        Task ToggleCompleted(TaskDto completedTask);
        Task DeactivateTask(TaskDto deactivatedTask);

    }
}
