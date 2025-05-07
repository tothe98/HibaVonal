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

        CreateMap<RoomEquipment, RoomEquipmentDto>().ReverseMap();
        CreateMap<RoomEquipment, RoomEquipmentCreateDto>().ReverseMap();
        CreateMap<RoomEquipment, RoomEquipmentUpdateDto>().ReverseMap();


        CreateMap<ErrorType, ErrorTypeDto>().ReverseMap();
        CreateMap<ErrorType, ErrorTypeCreateUpdateDto>().ReverseMap();

        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<OrderCreateDto, Order>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Items.Sum(i => i.Price * i.Quantity)))
            .ForMember(dest => dest.Items, opt => opt.Ignore());
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<OrderItemCreateDto, OrderItem>();

        //Kell ez ide egy�ltal�n
        //CreateMap<Room , RoomDto>().ReverseMap();
        CreateMap<Room, RoomDto>()
            .Include<SharedRoom, SharedRoomDto>()
            .Include<PersonalRoom, PersonalRoomDto>();
        CreateMap<PersonalRoomDto, PersonalRoom>().ReverseMap()
            .ForMember(dest => dest.RoomType, opt => opt.MapFrom(_ => "PersonalRoom"));
        CreateMap<SharedRoomDto, SharedRoom>().ReverseMap()
             .ForMember(dest => dest.RoomType, opt => opt.MapFrom(_ => "SharedRoom"));
        CreateMap<PersonalRoom, PersonalRoomCreateDto>().ReverseMap();
        CreateMap<SharedRoom, SharedRoomCreateDto>().ReverseMap();

        CreateMap<ErrorLog, ErrorLogDto>().ReverseMap();
        CreateMap<ErrorLog, ErrorLogCreateDto>().ReverseMap();
        CreateMap<ErrorLog, ErrorLogUpdateDto>().ReverseMap();
        CreateMap<ErrorLog, ErrorLogReporterUpdateDto>().ReverseMap();

        CreateMap<Role, RoleDto>().ReverseMap();

        CreateMap<User, UserDataDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<UserRole, UserRoleDto>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
    }
}
