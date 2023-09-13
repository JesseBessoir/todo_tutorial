using E3Starter.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Dtos
{
    public class TaskSearchCriteriaDto : BaseEntityDto
    {
        public bool CompletedAt { get; set; }

        public List<int> CatergoryIds { get; set; }
    }
}
