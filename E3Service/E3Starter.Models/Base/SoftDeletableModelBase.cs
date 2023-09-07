namespace E3Starter.Models.Base;

public class SoftDeletableModelBase : UserManagedModelBase
{
    public virtual DateTime? DeactivatedAt { get; set; }
    public virtual int? DeactivatedByUserId { get; set; }
}
