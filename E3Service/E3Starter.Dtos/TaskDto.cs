using E3Starter.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Dtos;

public class TaskDto
{
    public int Id { get; set; }
    public string TaskName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? DeactivatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public List<CategoriesDto> Categories { get; set; }

}
