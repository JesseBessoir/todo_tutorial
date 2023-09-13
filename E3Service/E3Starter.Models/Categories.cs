using E3Starter.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Models
{
    public class Categories : ModelBase
    {
        public virtual string CategoryName { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? DeactivatedAt { get; set; }

    }
}
