using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace Flats
{
    public class DomainProfile : Profile
    {

        public DomainProfile()
        {
            CreateMap<Apartments, ApartmentsDTO>()
                .ForMember(dest => dest.HouseStage,
                            opt => opt.MapFrom(src => src.House.StageNumber))
                .ForMember(dest => dest.HouseNumber,
                            opt => opt.MapFrom(src => src.House.HouseNumber))
                .ForMember(dest => dest.ComplexNameOfHouse,
                            opt => opt.MapFrom(src => src.House.ComplexName))
                .ForMember(dest => dest.DistrictName,
                            opt => opt.MapFrom(src => src.House.District.DistrictName))
                .ForMember(dest => dest.RegionName,
                            opt => opt.MapFrom(src => src.House.District.Region.RegionName))
                ;
            CreateMap<ApartmentsDTO, Apartments>();

        }


    }
}
