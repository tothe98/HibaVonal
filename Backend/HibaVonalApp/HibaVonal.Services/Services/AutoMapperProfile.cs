using AutoMapper;
using HibaVonal.DataContext.Dtos;
using Hibavonal.DataContext.Entities;

namespace HibaVonal.Services.Services;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<Address, AddressCreateDto>().ReverseMap();

        CreateMap<Dormitory, DormitoryDto>().ReverseMap();
        CreateMap<Dormitory, DormitoryCreateDto>().ReverseMap();

        CreateMap<Equipment, EquipmentDto>().ReverseMap();
        CreateMap<Equipment, EquipmentCreateDto>().ReverseMap();
        CreateMap<Equipment, EquipmentUpdateDto>().ReverseMap();

        CreateMap<ErrorType, ErrorTypeDto>().ReverseMap();
        CreateMap<ErrorType, ErrorTypeCreateUpdateDto>().ReverseMap();


    }
}
