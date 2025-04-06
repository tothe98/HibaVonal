using AutoMapper;
using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.DataContext.Dtos;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.Services.Services;

public interface IRoomService
{
    Task<List<RoomDto>> List();
    Task Create(RoomDto room);
    Task Update(int id, RoomDto room);
    Task Delete(int id);
}

public class RoomService : IRoomService
{
    private readonly SQL _context;
    private readonly IMapper _mapper;
    public RoomService(SQL context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private Room MapToRoomType(RoomDto room)
    {
        if (room is SharedRoomDto sharedRoom)
        {
            return _mapper.Map<SharedRoom>(sharedRoom);
        }
        else if (room is PersonalRoomDto personalRoom)
        {
            return _mapper.Map<PersonalRoom>(personalRoom);
        }

        throw new IncorrectRoomTypeException();
    }

    public async Task<List<RoomDto>> List()
    {
        List<RoomDto> roomDtos = new List<RoomDto>();
        var rooms = await _context.Room.Include(r => r.Dormitory).ToListAsync();
        rooms.ForEach(room =>
        {
            if (room is PersonalRoom personalRoom)
            {
                var result = _mapper.Map<PersonalRoomDto>(personalRoom);
                result.RoomType = "PersonalRoom";
                roomDtos.Add(result);

            }
            else if (room is SharedRoom sharedRoom)
            {
                var result = _mapper.Map<SharedRoomDto>(sharedRoom);
                result.RoomType = "SharedRoom";
                roomDtos.Add(result);
            }
        });

        return roomDtos;
    }

    public async Task Create(RoomDto room)
    {
        ObjectValidatorService<RoomDto> v = new ObjectValidatorService<RoomDto>(room);
        v.IsValid();
        if (!_context.Dormitory.Any(d => d.Id == room.DormitoryId))
        {
            throw new DormitoryWithIdNotExistsException();
        }
        if (room is PersonalRoomDto personalRoom)
        {
            ObjectValidatorService<PersonalRoomDto> pr = new ObjectValidatorService<PersonalRoomDto>(personalRoom);
            pr.IsValid();
            if (_context.Room.OfType<PersonalRoom>().Any(pr => pr.Number == personalRoom.Number))
            {
                throw new RoomWithNumberExistsException();
            }
            PersonalRoom newPersonalRoom = new PersonalRoom();
            newPersonalRoom.Number = personalRoom.Number;
           // newPersonalRoom.Residents = personalRoom.Residents;
            newPersonalRoom.Floor = personalRoom.Floor;
            newPersonalRoom.DormitoryId = personalRoom.DormitoryId;
            newPersonalRoom.Dormitory = personalRoom.Dormitory;
            newPersonalRoom.Equipments = personalRoom.Equipments;

            await _context.Room.AddAsync(newPersonalRoom);
            await _context.SaveChangesAsync();
        }
        if (room is SharedRoomDto sharedRoom)
        {
            ObjectValidatorService<SharedRoomDto> sr = new ObjectValidatorService<SharedRoomDto>(sharedRoom);
            sr.IsValid();
            SharedRoom newSharedRoom = new SharedRoom();
            newSharedRoom.Floor = sharedRoom.Floor;
            newSharedRoom.DormitoryId = sharedRoom.DormitoryId;
            newSharedRoom.Dormitory = sharedRoom.Dormitory;
            newSharedRoom.Equipments = sharedRoom.Equipments;
            newSharedRoom.PersonInCharge = sharedRoom.PersonInCharge;
            newSharedRoom.PersonInChargeContact = sharedRoom.PersonInChargeContact;
            await _context.Room.AddAsync(newSharedRoom);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Update(int id, RoomDto room)
    {
        //Itt valamiért nem jól ellenőrzi a required attribútumokat!!!!!!!!!!!!!!!!
        //És ha nem adod meg egyáltalán az adott attribútumot, akkor is az a default érték lesz
        ObjectValidatorService<RoomDto> v = new ObjectValidatorService<RoomDto>(room);
        v.IsValid();
        string givenroomtype = room.GetType().Name;
        Room oldRoom = _context.Room.AsNoTracking().FirstOrDefault(r => r.Id == id);
        if (oldRoom == null)
        {
            throw new RoomWithIdNotExistsException();
        }
        if (oldRoom.GetType().Name != givenroomtype)
        {
            throw new RoomRoomTypeNotMatchException();
        }
        if (!_context.Dormitory.Any(d => d.Id == room.DormitoryId))
        {
            throw new DormitoryWithIdNotExistsException();
        }
        if (room is PersonalRoomDto personalRoom)
        {
            ObjectValidatorService<PersonalRoomDto> pr = new ObjectValidatorService<PersonalRoomDto>(personalRoom);
            pr.IsValid();

            PersonalRoom oldPersonalRoom = (PersonalRoom)oldRoom;
            if (oldPersonalRoom.Number != personalRoom.Number && _context.Room.OfType<PersonalRoom>().Any(pr => pr.Number == personalRoom.Number))
            {
                throw new RoomWithNumberExistsException();
            }
            PersonalRoom newPersonalRoom = _context.PersonalRoom.First(r => r.Id == id);
            newPersonalRoom.Number = personalRoom.Number;
            //newPersonalRoom.Residents = personalRoom.Residents;
            newPersonalRoom.Floor = personalRoom.Floor;
            newPersonalRoom.DormitoryId = personalRoom.DormitoryId;
            newPersonalRoom.Dormitory = personalRoom.Dormitory;
            newPersonalRoom.Equipments = personalRoom.Equipments;
            _context.Room.Update(newPersonalRoom);
            await _context.SaveChangesAsync();
        }
        if (room is SharedRoomDto sharedRoom)
        {
            ObjectValidatorService<SharedRoomDto> sr = new ObjectValidatorService<SharedRoomDto>(sharedRoom);
            sr.IsValid();
            SharedRoom newSharedRoom = _context.SharedRoom.First(r => r.Id == id);
            newSharedRoom.Floor = sharedRoom.Floor;
            newSharedRoom.DormitoryId = sharedRoom.DormitoryId;
            newSharedRoom.Dormitory = sharedRoom.Dormitory;
            newSharedRoom.Equipments = sharedRoom.Equipments;
            newSharedRoom.PersonInCharge = sharedRoom.PersonInCharge;
            newSharedRoom.PersonInChargeContact = sharedRoom.PersonInChargeContact;
            _context.Room.Update(newSharedRoom);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Delete(int id)
    {
        var room = _context.Room.FirstOrDefault(d => d.Id == id);
        if (room == null)
        {
            throw new RoomWithIdNotExistsException();
        }
        _context.Room.Remove(room);
        await _context.SaveChangesAsync();
    }
}
