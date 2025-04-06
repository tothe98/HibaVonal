using AutoMapper;
using HibaVonal.DataContext.Dtos;
using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext.Entities;

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

        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<OrderCreateDto, Order>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Items.Sum(i => i.Price * i.Quantity)))
            .ForMember(dest => dest.Items, opt => opt.Ignore());
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<OrderItemCreateDto, OrderItem>();

        CreateMap<Room, RoomDto>().ReverseMap();


        CreateMap<ErrorLog, ErrorLogDto>().ReverseMap();
        CreateMap<ErrorLog, ErrorLogCreateDto>().ReverseMap();


        CreateMap<User, UserDataDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<UserRole, UserRoleDto>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
    }
}
