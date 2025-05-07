using AutoMapper;
using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.DataContext.Dtos;
using HibaVonal.DataContext.Entities;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Services.Services
{

    public interface IRoomEquipmentService
    {
        
        Task<List<RoomEquipmentDto>> List();

        Task<RoomEquipmentDto> CreateRoomEquipment(RoomEquipmentCreateDto roomEquipment);

        Task<RoomEquipmentDto> UpdateRoomEquipment( int RoomId, int EquipmentId ,RoomEquipmentUpdateDto roomEquipment);

        Task<bool> Delete(int RoomId, int EquipmentId);

    }
    public class RoomEquipmentService : IRoomEquipmentService
    {
        private readonly SQL _context;
        private readonly IMapper _mapper;
        public RoomEquipmentService(SQL context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoomEquipmentDto> CreateRoomEquipment(RoomEquipmentCreateDto roomEquipment)
        {
           if (! await _context.Room.AnyAsync(r => r.Id == roomEquipment.RoomId))
           {
                throw new RoomWithIdNotExistsException();
           }
           if (!await _context.Equipment.AnyAsync(r => r.Id == roomEquipment.EquipmentId))
           {
                throw new EquipmentWithIdNotExistsException();
           }
           if ( await _context.RoomEquipment.AnyAsync(r=>r.RoomId== roomEquipment.RoomId && r.EquipmentId == roomEquipment.RoomId))
           {
                throw new RoomEquipmentAlreadyExistsException();
           }
            var loadedRoomEquipment = await _context.RoomEquipment
             .Include(re => re.Room)
             .Include(re => re.Equipment)
             .FirstOrDefaultAsync(re => re.EquipmentId == roomEquipment.EquipmentId && re.RoomId == roomEquipment.RoomId);
            RoomEquipment roomEquipmentTrue = _mapper.Map<RoomEquipment>(loadedRoomEquipment);
            await _context.RoomEquipment.AddAsync(roomEquipmentTrue);
            await _context.SaveChangesAsync();
            return _mapper.Map<RoomEquipmentDto>(loadedRoomEquipment);


        }

        public async Task<bool> Delete(int RoomId , int EquipmentId)
        {
            if (!await _context.Room.AnyAsync(r => r.Id == RoomId))
            {
                throw new RoomWithIdNotExistsException();
            }
            if (!await _context.Equipment.AnyAsync(r => r.Id == EquipmentId))
            {
                throw new EquipmentWithIdNotExistsException();
            }
            var roomEquipmentWanted = await _context.RoomEquipment.FirstAsync(r => r.EquipmentId == EquipmentId && r.RoomId == RoomId);
            _context.RoomEquipment.Remove(roomEquipmentWanted);
            await _context.SaveChangesAsync() ;
            return true;
        }

        public async Task<List<RoomEquipmentDto>> List()
        {
            List<RoomEquipment> roomEquipment = new List<RoomEquipment>();
            var result = await _context.RoomEquipment.Include(r=>r.Room).Include(e=>e.Equipment).ToListAsync();
            var REResult = _mapper.Map<List<RoomEquipmentDto>>(result);
            return REResult;

        }

        public async Task<RoomEquipmentDto> UpdateRoomEquipment(int RoomId, int EquipmentId, RoomEquipmentUpdateDto roomEquipment)
        {
            if (!await _context.Room.AnyAsync(r => r.Id == RoomId))
            {
                throw new RoomWithIdNotExistsException();
            }
            if (!await _context.Equipment.AnyAsync(r => r.Id == EquipmentId))
            {
                throw new EquipmentWithIdNotExistsException();
            }
            if (!await _context.RoomEquipment.AnyAsync(r=>r.RoomId == RoomId && r.EquipmentId == EquipmentId)) 
            {
                throw new RoomEquipmentWithIdsNotExistsException();
            }
            var roomEquipmentTrue= await _context.RoomEquipment.Include(r=>r.Equipment).Include(e=>e.Room).FirstAsync(r=>r.RoomId==RoomId && r.EquipmentId== EquipmentId);
            _mapper.Map(roomEquipment, roomEquipmentTrue);
            _context.RoomEquipment.Update(roomEquipmentTrue);
            await _context.SaveChangesAsync();
            return _mapper.Map<RoomEquipmentDto>(await _context.RoomEquipment.Include(r=>r.Equipment).Include(r=>r.Room).FirstOrDefaultAsync(r=>r.RoomId==RoomId && r.EquipmentId==EquipmentId));

        }
    }
}
