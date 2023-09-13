using E3Starter.Models;
using E3Starter.Persistence.NHIbernate.Mappings.Base;
namespace E3Starter.Persistence.NHIbernate.Mappings;

public class CatergoriesMap : ModelBaseMap<Categories>
{
    public CatergoriesMap() : base("Categories")
    {
        Map(x => x.CategoryName);
        Map(x => x.CreatedAt);
        Map(x => x.DeactivatedAt).Nullable();
    }
}