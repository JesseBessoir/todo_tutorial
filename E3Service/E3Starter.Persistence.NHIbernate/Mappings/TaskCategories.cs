using E3Starter.Models;
using E3Starter.Persistence.NHIbernate.Mappings.Base;
namespace E3Starter.Persistence.NHIbernate.Mappings;

public class TaskCategoriesMap : ModelBaseMap<TaskCategories>
{
    public TaskCategoriesMap() : base("TaskCategories")
    {
        Map(x => x.TaskId);
        Map(x => x.CategoryId);
        Map(x => x.CreatedAt);
        Map(x => x.DeactivatedAt).Nullable();

    }
}