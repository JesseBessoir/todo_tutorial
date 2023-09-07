using E3Starter.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Dtos;

public class UserDto : BaseEntityDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public IList<RoleDto> Roles { get; set; }
}
