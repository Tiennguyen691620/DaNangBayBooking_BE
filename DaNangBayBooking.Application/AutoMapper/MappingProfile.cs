using AutoMapper;
using DaNangBayBooking.Data.Entities;
using DaNangBayBooking.Utilities.Extensions;
using DaNangBayBooking.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.Application.AutoMapper
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //EntityToDtoMappingConfiguration();
            /*DtoToEntityMappingConfiguration();
            DtoToDtoMappingConfiguration();*/
        }

        /*public void EntityToDtoMappingConfiguration()
        {
            CreateMap<AppUser, CreateAdminRequest>()
                .ForMember(dto => dto.Dob, entity => entity.MapFrom(prop => prop.Dob.FromUnixTimeStamp()));
        }*/
    }
}
