using AutoMapper;
using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.DataContext.Dtos;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.Services.Services;

public interface IRoomService
{
    Task<IEnumerable<RoomDto>> List();
    Task<PersonalRoomDto> CreatePersonalRoom(PersonalRoomCreateDto room);
    Task<SharedRoomDto> CreateSharedRoom(SharedRoomCreateDto room);
    Task<PersonalRoomDto> UpdatePersonalRoom(int id, PersonalRoomCreateDto room);
    Task<SharedRoomDto> UpdateSharedRoom(int id, SharedRoomCreateDto room);
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

    public async Task<IEnumerable<RoomDto>> List()
    {
        List<Room> rooms = new List<Room>();
        var result = await _context.Room.Include(r => r.Dormitory).ToListAsync();
        result.ForEach(room =>
        {
            if (room is PersonalRoom personalRoom)
            {
                rooms.Add((PersonalRoom)room);
            }
            else if (room is SharedRoom sharedRoom)
            {
                rooms.Add((SharedRoom)sharedRoom);
            }
        });

        var roomsDto = _mapper.Map<IEnumerable<RoomDto>>(rooms);
        return roomsDto;
    }

    public async Task<PersonalRoomDto> CreatePersonalRoom(PersonalRoomCreateDto room)
    {
        if (!_context.Dormitory.Any(d => d.Id == room.DormitoryId))
        {
            throw new DormitoryWithIdNotExistsException();
        }
        if (_context.Room.OfType<PersonalRoom>().Any(pr => pr.Number == room.Number))
        {
            throw new RoomWithNumberExistsException();
        }
        var result = await _context.Room.AddAsync(_mapper.Map<PersonalRoom>(room));
        await _context.SaveChangesAsync();

        return _mapper.Map<PersonalRoomDto>(await _context.Room.OfType<PersonalRoom>().Include(d => d.Dormitory).FirstOrDefaultAsync(r => r.Id == result.Entity.Id));
    }

    public async Task<SharedRoomDto> CreateSharedRoom(SharedRoomCreateDto room)
    {
        if (!_context.Dormitory.Any(d => d.Id == room.DormitoryId))
        {
            throw new DormitoryWithIdNotExistsException();
        }
        var result = await _context.Room.AddAsync(_mapper.Map<SharedRoom>(room));
        await _context.SaveChangesAsync();

        return _mapper.Map<SharedRoomDto>(await _context.Room.OfType<SharedRoom>().Include(d=>d.Dormitory).FirstOrDefaultAsync(r => r.Id == result.Entity.Id));
    }

    public async Task<PersonalRoomDto> UpdatePersonalRoom(int id, PersonalRoomCreateDto room)
    {
        PersonalRoom oldRoom = await _context.Room.OfType<PersonalRoom>().FirstOrDefaultAsync(r => r.Id == id);
        if (oldRoom == null)
        {
            throw new RoomWithIdNotExistsException();
        }
        if (!_context.Dormitory.Any(d => d.Id == room.DormitoryId))
        {
            throw new DormitoryWithIdNotExistsException();
        }
        if (oldRoom.Number != room.Number && _context.Room.OfType<PersonalRoom>().Any(pr => pr.Number == room.Number))
        {
            throw new RoomWithNumberExistsException();
        }
        _mapper.Map(room, oldRoom);
        _context.Room.Update(oldRoom);
        await _context.SaveChangesAsync();

        return _mapper.Map<PersonalRoomDto>(await _context.Room.OfType<PersonalRoom>().Include(d => d.Dormitory).FirstOrDefaultAsync(r => r.Id == id));
    }
    public async Task<SharedRoomDto> UpdateSharedRoom(int id, SharedRoomCreateDto room)
    {
        SharedRoom oldRoom = await _context.Room.OfType<SharedRoom>().FirstOrDefaultAsync(r => r.Id == id);
        if (oldRoom == null)
        {
            throw new RoomWithIdNotExistsException();
        }
        if (!_context.Dormitory.Any(d => d.Id == room.DormitoryId))
        {
            throw new DormitoryWithIdNotExistsException();
        }
        _mapper.Map(room, oldRoom);
        _context.Room.Update(oldRoom);
        await _context.SaveChangesAsync();

        return _mapper.Map<SharedRoomDto>(await _context.Room.OfType<SharedRoom>().Include(d => d.Dormitory).FirstOrDefaultAsync(r => r.Id == id));
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
