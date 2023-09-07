using E3Starter.Models;
using E3Starter.Persistence.NHIbernate.Mappings.Base;

namespace E3Starter.Persistence.NHIbernate.Mappings;

public class RoleMap : ModelBaseMap<Role>
{
    public RoleMap() : base("Roles")
    {
        Map(x => x.Name);
        HasManyToMany(x => x.Users)
            .Cascade.All()
            .Inverse()
            .Table("UserRoles")
            .ParentKeyColumn("RoleId")
            .ChildKeyColumn("UserId");
    }
}
