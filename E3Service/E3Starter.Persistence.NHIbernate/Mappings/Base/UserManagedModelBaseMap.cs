using E3Starter.Models.Base;

namespace E3Starter.Persistence.NHIbernate.Mappings.Base;

public abstract class UserManagedModelBaseMap<T> : ModelBaseMap<T> where T : UserManagedModelBase
{
    public UserManagedModelBaseMap(string tableName) : base(tableName)
    {
        Map(x => x.CreatedAt).Nullable();
        Map(x => x.CreatedByUserId).Nullable();
    }
}
