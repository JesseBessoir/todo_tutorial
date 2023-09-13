using E3Starter;
using E3Starter.Dtos;
using E3Starter.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using E3Starter.Persistence.NHIbernate.Mappings;

namespace LegacyFitness.ModelDtoMappings
{
    public class ModelDtoMapperProfile : Profile
    {
        public ModelDtoMapperProfile()
        {

            CreateMap<Tasks, TaskDto>();
            CreateMap<List<Tasks>, List<TaskDto>>();

            //Mapping TaskCategories and Categories to Respective Dtos
            CreateMap<Categories, CategoriesDto>();
            CreateMap<List<Categories>, List<CategoriesDto>>();

            CreateMap<TaskCategories, TaskCategoriesDto>();
            CreateMap<List<TaskCategories>, List<TaskCategoriesDto>>();

        }
    }
}