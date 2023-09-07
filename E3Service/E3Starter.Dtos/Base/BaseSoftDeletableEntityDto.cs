using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Dtos.Base
{
    public abstract class BaseSoftDeletableEntityDto : BaseUserManagedEntityDto
    {
        public DateTime? DeactivatedAt { get; set; }
        public int? DeactivatedByUserId { get; set; }
    }
}
