using E3Starter.Models.Base;

namespace E3Starter.Models;

public class Tasks : ModelBase
{
    public virtual string TaskName { get; set; }
    public virtual DateTime? DeletedAt { get; set; }
    public virtual DateTime? CompletedAt { get; set; }
    public virtual DateTime CreatedAt { get; set; }
    public virtual DateTime? DeactivatedAt { get; set; }
    public virtual int PriorityId { get; set; }

}
