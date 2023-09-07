using E3Starter.Models.Base;

namespace E3Starter.Persistence.NHIbernate.Mappings.Base;

public abstract class SoftDeleteableModelBaseMap<T> : UserManagedModelBaseMap<T> where T : SoftDeletableModelBase
{
    public SoftDeleteableModelBaseMap(string tableName) : base(tableName)
    {
        Map(x => x.DeactivatedAt).Nullable();
        Map(x => x.DeactivatedByUserId).Nullable();
    }
}
