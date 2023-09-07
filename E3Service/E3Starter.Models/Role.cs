using E3Starter.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Models;

public class Role : ModelBase
{
    public virtual string Name { get; set; }
    public virtual IList<User> Users { get; set; }
}
