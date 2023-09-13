using E3Starter.Contracts.Persistence;
using E3Starter.Dtos;
using E3Starter.Models;
using E3Starter.Persistence.NHIbernate.Repositories.Base;
using Microsoft.AspNetCore.Http;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Transform;
using System;
using System.Data;
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

    public Task<List<Categories>> GetAllCategories(TaskSearchCriteriaDto criteria)
    {

        var query = Session.Query<Categories>();
        query = query.Where(c => c.DeactivatedAt == null).OrderBy(c => c.Id);
        return query.ToListAsync();
    }

    public Task<List<Tasks>> GetAllTasks(TaskSearchCriteriaDto criteria)
    {

        var query = Session.Query<Tasks>();
        //var query = (from t in Session.Query<Tasks>()
        //             join tc in Session.Query<TaskCategories>() on t.Id equals tc.TaskId
        //             group tc by t into grouping
        //             select new { Task = grouping.Key, TaskCategories = grouping.DefaultIfEmpty() });
        if (criteria.CompletedAt == true)
        {
            query = query.Where(t => t.DeactivatedAt == null && t.DeletedAt == null && t.CompletedAt != null).OrderBy(t => t.CreatedAt);
        }
        else
        {
            query = query.Where(x => x.DeactivatedAt == null && x.DeletedAt == null && x.CompletedAt == null).OrderBy(x => x.CreatedAt);
        }

        //if (criteria.CatergoryIds != null && criteria.CatergoryIds.Any())
        //{
        //    query = query.Where(t => criteria.CatergoryIds
        //        .All(catId => t.Categories
        //            .Select(x => x.Id)
        //        .ToList()
        //        .Contains(catId)));
        //}

        return query.ToListAsync();
    }

    public async Task ToggleTaskComplete(TaskDto completedTask)
    {
        if (completedTask.CompletedAt == null)
        {
            await Session.CreateSQLQuery(@"
            UPDATE dbo.Tasks SET CompletedAt = GETUTCDATE() WHERE Id = :taskId")
            .SetInt32("taskId", completedTask.Id)
            .ExecuteUpdateAsync();
        }
        else
        {
            await Session.CreateSQLQuery(@"
            UPDATE dbo.Tasks SET CompletedAt = null where Id = :taskId")
            .SetInt32("taskId", completedTask.Id)
            .ExecuteUpdateAsync();
        }

    }
    public async Task DeactivateTask(TaskDto deactivatedTask)
    {
        await Session.CreateSQLQuery(@"
        UPDATE dbo.Tasks SET DeactivatedAt = GETUTCDATE() WHERE Id = :taskId")
        .SetInt32("taskId", deactivatedTask.Id)
        .ExecuteUpdateAsync();
    }
}
