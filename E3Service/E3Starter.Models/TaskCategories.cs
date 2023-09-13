using E3Starter.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Models
{
    public class TaskCategories : ModelBase
    {
        public virtual int TaskId { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? DeactivatedAt { get; set; }

    }
}
