using E3Starter.Dtos;
using E3Starter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Contracts.Persistence;

public interface IReferenceRepository : IRepositoryBase
{
    Task<List<Role>> GetRolesAsync();
    Task<List<Tasks>> GetAllTasks(bool completedAt);
    Task<List<Priority>> GetPriorityList();
    Task ToggleTaskComplete(TaskDto completedTask);
    Task DeactivateTask(TaskDto deactivatedTask);

}
