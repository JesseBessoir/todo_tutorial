using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Dtos.Base
{
    public abstract class BaseUserManagedEntityDto : BaseEntityDto
    {
        public DateTime? CreatedAt { get; set; }
        public int? CreatedByUserId { get; set; }
    }
}
