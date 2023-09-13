using E3Starter.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Dtos
{
    public class CategoriesDto : BaseEntityDto
    {
        public  string CategoryName { get; set; }
        public  DateTime CreatedAt { get; set; }
        public  DateTime? DeactivatedAt { get; set; }
    }
}
