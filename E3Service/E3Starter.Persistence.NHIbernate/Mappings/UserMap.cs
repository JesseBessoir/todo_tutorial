using E3Starter.Models;
using E3Starter.Persistence.NHIbernate.Mappings.Base;

namespace E3Starter.Persistence.NHIbernate.Mappings;

public class UserMap : SoftDeleteableModelBaseMap<User>
{
    public UserMap() : base("Users")
    {
        Map(x => x.Username);
        Map(x => x.Email);
        Map(x => x.HashedPassword);
        Map(x => x.PasswordSalt);
        HasManyToMany(x => x.Roles)
            .Cascade.All()
            .Table("UserRoles")
            .ParentKeyColumn("UserId")
            .ChildKeyColumn("RoleId");
    }
}
