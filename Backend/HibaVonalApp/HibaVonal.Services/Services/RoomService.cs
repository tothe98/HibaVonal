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
    Task CreatePersonalRoom(PersonalRoomCreateDto room);
    Task CreateSharedRoom(SharedRoomCreateDto room);
    Task UpdatePersonalRoom(int id, PersonalRoomCreateDto room);
    Task UpdateSharedRoom(int id, SharedRoomCreateDto room);
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
        List<RoomDto> roomDtos = new List<RoomDto>();
        var rooms = await _context.Room.Include(r => r.Dormitory).ToListAsync();
        rooms.ForEach(room =>
        {
            if (room is PersonalRoom personalRoom)
            {
                var result = _mapper.Map<PersonalRoomDto>((PersonalRoom)personalRoom);
                result.RoomType = "PersonalRoom";
                roomDtos.Add(result);

            }
            else if (room is SharedRoom sharedRoom)
            {
                var result = _mapper.Map<SharedRoomDto>((SharedRoom)sharedRoom);
                result.RoomType = "SharedRoom";
                roomDtos.Add(result);
            }
        });
        IEnumerable<RoomDto> res = roomDtos;
        return res;
    }

    public async Task CreatePersonalRoom(PersonalRoomCreateDto room)
    {
        if (!_context.Dormitory.Any(d => d.Id == room.DormitoryId))
        {
            throw new DormitoryWithIdNotExistsException();
        }
        if (_context.Room.OfType<PersonalRoom>().Any(pr => pr.Number == room.Number))
        {
            throw new RoomWithNumberExistsException();
        }
        await _context.Room.AddAsync(_mapper.Map<PersonalRoom>(room));
        await _context.SaveChangesAsync();
    }

    public async Task CreateSharedRoom(SharedRoomCreateDto room)
    {
        if (!_context.Dormitory.Any(d => d.Id == room.DormitoryId))
        {
            throw new DormitoryWithIdNotExistsException();
        }
        await _context.Room.AddAsync(_mapper.Map<SharedRoom>(room));
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePersonalRoom(int id, PersonalRoomCreateDto room)
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
    }
    public async Task UpdateSharedRoom(int id, SharedRoomCreateDto room)
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
