using AutoMapper;
using FribergWebAPI.DTOs;
using FribergWebAPI.Models;

//author: Johan Krångh, Pontus Lerman, Christian Alp
namespace FribergWebAPI.Profiles
{
	public class DtoProfile : Profile
	{
		public DtoProfile()
		{
			CreateMap<Realtor, RealtorDto>().ReverseMap();
			CreateMap<Agency, RealtorAgencyDto>().ReverseMap();
			CreateMap<Residence, ResidenceDto>().ReverseMap();
			CreateMap<Residence, CRUDResidenceDto>().ReverseMap();
			CreateMap<Agency, AgencyDto>().ReverseMap();
			CreateMap<Category, CategoryDto>().ReverseMap();
			CreateMap<Municipality, MunicipalityDto>().ReverseMap();
		}
	}
}
