using E3Starter.Models;
using E3Starter.Persistence.NHIbernate.Mappings.Base;
namespace E3Starter.Persistence.NHIbernate.Mappings;

public class TaskMap : ModelBaseMap<Tasks>
{
    public TaskMap() : base("Tasks")
    {
        Map(x => x.TaskName);
        Map(x => x.CompletedAt).Nullable();
        Map(x => x.CreatedAt);
        Map(x => x.DeactivatedAt).Nullable();
        Map(x => x.DeletedAt).Nullable();
    }
}