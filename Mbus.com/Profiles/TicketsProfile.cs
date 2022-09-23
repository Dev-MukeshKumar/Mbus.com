using AutoMapper;
using Mbus.com.Models;
using System;

namespace Mbus.com.Profiles
{
    public class TicketsProfile: Profile
    {
        public TicketsProfile()
        {
            CreateMap<Entities.Ticket, TicketDTO>().ReverseMap();
            CreateMap<Entities.Ticket, UserTicketToReturnDTO>()
                .ForMember(
                    dest => dest.TravelDate,
                    opt => opt.MapFrom(src => src.TravelDate.ToString("d-M-yyyy - H:mm")))
                .ForMember(
                    dest => dest.BookedDate,
                    opt => opt.MapFrom(src => src.BookedDate.ToString("d-M-yyyy - H:mm")));
            CreateMap<Entities.Ticket, OwnerTicketToReturnDTO>()
                .ForMember(
                    dest => dest.TravelDate,
                    opt => opt.MapFrom(src => src.TravelDate.ToString("d-M-yyyy - H:mm")))
                .ForMember(
                    dest => dest.BookedDate,
                    opt => opt.MapFrom(src => src.BookedDate.ToString("d-M-yyyy - H:mm")));
            CreateMap<TicketCreationDTO, Entities.Ticket>().ForMember(
                dest => dest.TravelDate,
                opt => opt.MapFrom(src => DateTime.Parse(src.TravelDate))
                );
        }
    }
}
