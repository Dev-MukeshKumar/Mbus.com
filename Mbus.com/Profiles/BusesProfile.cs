using AutoMapper;
using Mbus.com.Entities;
using Mbus.com.Models;
using System;
using System.Globalization;

namespace Mbus.com.Profiles
{
    public class BusesProfile: Profile
    {
        public BusesProfile()
        {
            CreateMap<Bus, BusDTO>().ReverseMap();
            CreateMap<Bus, BusToReturnDTO>().ForMember(
                dest => dest.DepartureTime,
                opt => opt.MapFrom(src => src.DepartureTime.ToString("H:mm"))
                );
            CreateMap<BusCreationDTO, Bus>().ForMember(
                    dest => dest.DepartureTime,
                    opt => opt.MapFrom(src =>  DateTime.Parse(src.DepartureTime))
                );
        }
    }
}
