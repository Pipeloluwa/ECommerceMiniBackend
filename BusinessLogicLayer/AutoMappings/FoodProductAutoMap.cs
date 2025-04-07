using AutoMapper;
using DomainLayer.DTOs;
using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer.AutoMappings
{
    public class FoodProductAutoMap: Profile
    {

        public FoodProductAutoMap() 
        {
            CreateMap<FoodProductEntity, FoodProductResponseDTO>();
        }
    }
}
