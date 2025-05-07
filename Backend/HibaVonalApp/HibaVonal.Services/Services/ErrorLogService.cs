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
    public interface IErrorLogService
    {
        Task<List<ErrorLogDto>> List();
        Task<List<ErrorLogDto>> ListCurrent(int userId);
        Task<List<ErrorLogDto>> ListCurrentAssigned(int workerId);
        Task<ErrorLogDto> Create(int userId, ErrorLogCreateDto errorLogCreateDto);
        Task<ErrorLogDto> Update(int id, ErrorLogUpdateDto errorLogUpdateDto);
        Task Delete(int id);
    }
    public class ErrorLogService : IErrorLogService
    {
        private readonly SQL _context;
        private readonly IMapper _mapper;
        public ErrorLogService(SQL context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ErrorLogDto>> List()
        {
            return await _context.ErrorLog
                .Include(e => e.Room)
                .Include(e => e.MaintenanceWorker)
                .Include(e => e.Reporter)
                .Select(e => _mapper.Map<ErrorLogDto>(e))
                .ToListAsync();
        }

        public async Task<List<ErrorLogDto>> ListCurrentAssigned(int workerId)
        {
            return await _context.ErrorLog
                .Where(e => e.MaintenanceWorkerId == workerId)
                .Include(e => e.Room)
                .Include(e => e.MaintenanceWorker)
                .Include(e => e.Reporter)
                .Select(e => _mapper.Map<ErrorLogDto>(e))
                .ToListAsync();
        }

        public async Task<List<ErrorLogDto>> ListCurrent(int userId)
        {
            return await _context.ErrorLog
                .Where(e => e.ReporterId == userId)
                .Include(e => e.Room)
                .Include(e => e.MaintenanceWorker)
                .Include(e => e.Reporter)
                .Select(e => _mapper.Map<ErrorLogDto>(e))
                .ToListAsync();
        }

        public async Task<ErrorLogDto> Create(int userId, ErrorLogCreateDto errorLogCreateDto)
        {
            var errorlog = _mapper.Map<ErrorLog>(errorLogCreateDto);
            var reporter = await _context.User.FindAsync(userId);
            if (reporter == null)
            {
                throw new ReporterWithIdNotExistsException("Reporter not found");
            }
            /*var maintenanceWorker = await _context.User.FindAsync(userId);
            if (maintenanceWorker == null)
            {
                throw new MaintenanceWorkerWithIdNotExistsException("Maintenance worker not found");
            }*/
            var room = await _context.Room.FindAsync(errorLogCreateDto.RoomId);
            if (room == null)
            {
                throw new RoomWithIdNotExistsException();
            }

            errorlog.Reporter = reporter;
            //errorlog.MaintenanceWorker = maintenanceWorker;
            errorlog.Status = EErrorStatus.Recieved;
            errorlog.Room = room;
            errorlog.ReportTime = DateTime.Now;

            await _context.ErrorLog.AddAsync(errorlog);
            await _context.SaveChangesAsync();
            return _mapper.Map<ErrorLogDto>(errorlog);
        }
        public async Task<ErrorLogDto> Update(int id, ErrorLogUpdateDto errorLogUpdateDto)
        {
            var errorLog = await _context.ErrorLog.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (errorLog == null)
            {
                throw new ErrorLogWithIdNotExistsException("Error log not found");
            }
            var reporter = _context.User.Find(errorLogUpdateDto.ReporterId);
            if (reporter == null)
            {
                throw new ReporterWithIdNotExistsException("Reporter not found");
            }
            var maintenanceWorker = _context.User.Find(errorLogUpdateDto.MaintenanceWorkerId);
            if (maintenanceWorker == null)
            {
                throw new MaintenanceWorkerWithIdNotExistsException("Maintenance worker not found");
            }
            var room = _context.Room.Find(errorLogUpdateDto.RoomId);
            if (room == null)
            {
                throw new RoomWithIdNotExistsException();
            }
            _mapper.Map(errorLogUpdateDto, errorLog);
            errorLog.Reporter = reporter;
            errorLog.MaintenanceWorker = maintenanceWorker;
            errorLog.Comment = errorLogUpdateDto.Comment;
            errorLog.Room = room;

            _context.ErrorLog.Update(errorLog);
            await _context.SaveChangesAsync();
            return _mapper.Map<ErrorLogDto>(errorLog);
        }

        public async Task Delete(int id)
        {
            var errorLog = _context.ErrorLog.Find(id);
            if (errorLog == null)
            {
                throw new ErrorLogWithIdNotExistsException("Error log not found");
            }
            _context.ErrorLog.Remove(errorLog);
            await _context.SaveChangesAsync();
        }

    }
}
