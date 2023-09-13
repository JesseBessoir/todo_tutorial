using E3Starter.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Models;

public class Priority : ModelBase
{
    public virtual string Name { get; set; }
    public virtual int Sequence { get; set; }
}
