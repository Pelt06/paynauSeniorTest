using AutoMapper;
using Backend.Application.Database.Product.Models;
using Backend.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ProductEntity, CreateProductModel>().ReverseMap();
            CreateMap<ProductEntity, UpdateProductModel>().ReverseMap();
            //CreateMap<PersonEntity, GetAllPeopleModel>().ReverseMap();
        }
    }
}
