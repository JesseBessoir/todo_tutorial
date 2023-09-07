using E3Starter.Models.Base;

namespace E3Starter.Models;

public class User : SoftDeletableModelBase
{
    public virtual string Username { get; set; }
    public virtual string Email { get; set; }
    public virtual string HashedPassword { get; set; }
    public virtual string PasswordSalt { get; set; }
    public virtual IList<Role> Roles { get; set; }
}
