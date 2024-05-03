using AutoMapper;
using FribergWebAPI.DTOs;
using FribergWebAPI.Models;

namespace FribergWebAPI.Profiles
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<Realtor, RealtorDto>();
            CreateMap<Agency, RealtorAgencyDto>();
            CreateMap<Residence, ResidenceDto>();
            CreateMap<Agency, CRUDAgencyDto>().ReverseMap();
        }
    }
}
