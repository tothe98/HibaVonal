using AutoMapper;
using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.DataContext.Dtos;
using HibaVonal.DataContext.Entities;
using HibaVonal.Services.Exceptions;
using HibaVonal.Services.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;

namespace HibaVonal.Services.Services
{

    public interface IUserService
    {
        Task<List<UserDataDto>> List();
        Task<UserDataDto> GetById(int id);
        Task<UserDataDto> GetByEmail(string email);
        Task<UserDataDto> Update(int userId, UserUpdateDto user);
        Task<UserDataDto> PasswordChange(int userId, PasswordChangeDto user);
        Task Delete(int id);
    }

    public class UserService : IUserService
    {
        private readonly SQL _context;
        private readonly IMapper _mapper;
        public UserService(SQL context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserDataDto>> List()
        {
            return await _context.User.Include(r=>r.Roles).ThenInclude(ro=>ro.Role).Select(u=>_mapper.Map<UserDataDto>(u)).ToListAsync();
        }

        public async Task<UserDataDto> GetById(int id)
        {
            User user = await _context.User.Include(u=>u.Roles).ThenInclude(r=>r.Role).FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            { 
                return _mapper.Map<UserDataDto>(user);
            }
            else
            {
                throw new NotFoundException("User is not found!");
            }
        }

        public async Task<UserDataDto> GetByEmail(string email)
        {
            User user = await _context.User.Include(r => r.Roles).ThenInclude(ro => ro.Role).FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                return _mapper.Map<UserDataDto>(user);
            }
            else
            {
                throw new NotFoundException("User is not found!");
            }
        }

        public async Task<UserDataDto> Update(int userId, UserUpdateDto user)
        {
            User updatingUser = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
            if (updatingUser == null)
            {
                throw new NotFoundException("User is not found!");
            }
            if (_context.User.Any(u => u.Email == user.Email && updatingUser.Id!=u.Id))
            {
                throw new EmailUsedException();
            }
            updatingUser.Name = user.Name;
            updatingUser.Email = user.Email;
            updatingUser.PhoneNumber = user.PhoneNumber;
            updatingUser.PersonalRoomId = user.PersonalRoomId;
            await _context.SaveChangesAsync();
            var updatedUser = await _context.User.Include(r => r.Roles).ThenInclude(ro => ro.Role).FirstOrDefaultAsync(u => u.Id == userId);
            return _mapper.Map<UserDataDto>(updatedUser);
        }

        public async Task<UserDataDto> PasswordChange(int userId, PasswordChangeDto passwordChangeDto)
        {
            User updatingUser = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
            if (updatingUser == null)
            {
                throw new NotFoundException("User is not found!");
            }
            Encryption enc = Encryption.Initialize(updatingUser.Password);
            if (enc.Validate(passwordChangeDto.CurrentPassword))
            {
                if (passwordChangeDto.NewPassword != passwordChangeDto.NewPasswordConfirm)
                {
                    throw new PasswordsNotMatchException();
                }
                else
                {
                    updatingUser.Password = enc.EncyptPassword(passwordChangeDto.NewPassword);
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
            await _context.SaveChangesAsync();
            var updatedUser = await _context.User.Include(r => r.Roles).ThenInclude(ro => ro.Role).FirstOrDefaultAsync(u => u.Id == userId);
            return _mapper.Map<UserDataDto>(updatedUser);
        }

        public async Task Delete(int id)
        {
            User user = await _context.User.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new NotFoundException("User is not found!");
            }
            user.IsDeleted = true;
            await _context.SaveChangesAsync();
        }


    }
}
