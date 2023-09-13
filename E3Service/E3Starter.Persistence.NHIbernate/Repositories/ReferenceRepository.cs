using E3Starter.Contracts.Persistence;
using E3Starter.Dtos;
using E3Starter.Models;
using E3Starter.Persistence.NHIbernate.Repositories.Base;
using Microsoft.AspNetCore.Http;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Persistence.NHIbernate.Repositories;

public class ReferenceRepository : RepositoryBase, IReferenceRepository
{
    public ReferenceRepository(NHibernate.ISession session) : base(session)
    {
    }

    public async Task<List<Role>> GetRolesAsync()
    {
        return await Session.Query<Role>().ToListAsync();
    } 
    //public async Task<List<TaskDto>> GetAllTasks()
    //{
    //    return await Session.Query<TaskDto>().ToListAsync();
    //}

    public Task<List<Tasks>> GetAllTasks(bool completedAt)
    {
        var query = Session.Query<Tasks>();
        {
            if (completedAt)
            {
                query = query.Where(t => t.CompletedAt != null && t.DeletedAt == null && t.DeactivatedAt == null).OrderBy(x => x.PriorityId);
            }
            else
            {
                query = query.Where(x => x.DeactivatedAt == null && x.DeletedAt == null && x.CompletedAt == null).OrderBy(x => x.PriorityId); 
            }
        }
        return query.ToListAsync();
    }
    public async Task<List<Priority>> GetPriorityList()
    {
        var query = Session.Query<Priority>();
        return await query.ToListAsync();
    }

    public async Task TaskPriority(TaskDto task)
    {
        await Session.CreateSQLQuery(@"
            UPDATE dbo.Tasks SET PriorityId = :priorityId WHERE Id = :taskId")
            .SetInt32("taskId", task.Id)
            .SetInt32("priorityId", task.PriorityId)
            .ExecuteUpdateAsync();
    }

    public async Task ToggleTaskComplete(TaskDto completedTask) {
        if (completedTask.CompletedAt == null)
        {
            await Session.CreateSQLQuery(@"
            UPDATE dbo.Tasks SET CompletedAt = GETUTCDATE() WHERE Id = :taskId")
            .SetInt32("taskId", completedTask.Id)
            .ExecuteUpdateAsync();
        }
        else {
            await Session.CreateSQLQuery(@"
            UPDATE dbo.Tasks SET CompletedAt = null where Id = :taskId")
            .SetInt32("taskId", completedTask.Id)
            .ExecuteUpdateAsync();
        }
        
    }
    public async Task DeactivateTask(TaskDto deactivatedTask) {
        await Session.CreateSQLQuery(@"
        UPDATE dbo.Tasks SET DeactivatedAt = GETUTCDATE() WHERE Id = :taskId")
        .SetInt32("taskId", deactivatedTask.Id)
        .ExecuteUpdateAsync();
    }

}
