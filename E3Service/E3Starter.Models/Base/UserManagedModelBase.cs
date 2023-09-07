using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Models.Base
{
    public class UserManagedModelBase : ModelBase
    {
        public virtual DateTime? CreatedAt { get; set; }
        public virtual int? CreatedByUserId { get; set; }
    }
}
