using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Dtos
{
    public class TaskCategoriesDto
    {
        public  IList<TaskDto> TaskId { get; set; }
        public  IList<CategoriesDto> CategoryId { get; set; }
        public  DateTime CreatedAt { get; set; }
        public  DateTime? DeactivatedAt { get; set; }
    }
}
