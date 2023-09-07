using E3Starter;
using E3Starter.Dtos;
using E3Starter.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace LegacyFitness.ModelDtoMappings
{
    public class ModelDtoMapperProfile : Profile
    {
        public ModelDtoMapperProfile()
        {

            CreateMap<Tasks, TaskDto>();
            CreateMap<List<Tasks>, List<TaskDto>>();
        
        }
    }
}