using AutoMapper;
using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.Services.Services;

public interface IRoomService
{
    Task<IEnumerable<Room>> List();
    Task Create(Room room);
    Task Update(Room room);
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

    private static Room MapToRoomType(Room room)
    {
        if (room is SharedRoom sharedRoom)
        {
            return sharedRoom;
        }
        else if (room is PersonalRoom personalRoom)
        {
            return personalRoom;
        }

        throw new IncorrectRoomTypeException();
    }

    public async Task<IEnumerable<Room>> List()
    {
        var rooms = await _context.Room.Include(r => r.Dormitory).ToListAsync();

        return rooms.Select(room => MapToRoomType(room)).ToList();
    }

    public async Task Create(Room room)
    {
        ObjectValidatorService<Room> v = new ObjectValidatorService<Room>(room);
        v.IsValid();
        if (!_context.Dormitory.Any(d => d.Id == room.DormitoryId))
        {
            throw new DormitoryWithIdNotExistsException();
        }
        if (room is PersonalRoom personalRoom)
        {
            ObjectValidatorService<PersonalRoom> pr = new ObjectValidatorService<PersonalRoom>(personalRoom);
            pr.IsValid();
            if (_context.Room.OfType<PersonalRoom>().Any(pr => pr.Number == personalRoom.Number))
            {
                throw new RoomWithNumberExistsException();
            }
            await _context.Room.AddAsync(personalRoom);
            await _context.SaveChangesAsync();
        }
        if (room is SharedRoom sharedRoom)
        {
            ObjectValidatorService<SharedRoom> sr = new ObjectValidatorService<SharedRoom>(sharedRoom);
            sr.IsValid();
            await _context.Room.AddAsync(sharedRoom);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Update(Room room)
    {
        //Itt valamiért nem jól ellenőrzi a required attribútumokat!!!!!!!!!!!!!!!!
        //És ha nem adod meg egyáltalán az adott attribútumot, akkor is az a default érték lesz
        ObjectValidatorService<Room> v = new ObjectValidatorService<Room>(room);
        v.IsValid();
        string givenroomtype = room.GetType().Name;
        Room oldRoom = _context.Room.AsNoTracking().FirstOrDefault(r => r.Id == room.Id);
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
        if (room is PersonalRoom personalRoom)
        {
            ObjectValidatorService<PersonalRoom> pr = new ObjectValidatorService<PersonalRoom>(personalRoom);
            pr.IsValid();

            PersonalRoom oldPersonalRoom = (PersonalRoom)oldRoom;
            if (oldPersonalRoom.Number != personalRoom.Number && _context.Room.OfType<PersonalRoom>().Any(pr => pr.Number == personalRoom.Number))
            {
                throw new RoomWithNumberExistsException();
            }
            _context.Room.Update(personalRoom);
            await _context.SaveChangesAsync();
        }
        if (room is SharedRoom sharedRoom)
        {
            ObjectValidatorService<SharedRoom> sr = new ObjectValidatorService<SharedRoom>(sharedRoom);
            sr.IsValid();
            _context.Room.Update(sharedRoom);
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
