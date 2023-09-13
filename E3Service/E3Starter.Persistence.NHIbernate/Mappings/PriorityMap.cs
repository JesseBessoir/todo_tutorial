using E3Starter.Models;
using E3Starter.Persistence.NHIbernate.Mappings.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Persistence.NHIbernate.Mappings;

public class PriorityMap : ModelBaseMap<Priority>
{
    public PriorityMap() : base("Priority")
    {
        Map(x => x.Name);
        Map(x => x.Sequence);
    }
}
