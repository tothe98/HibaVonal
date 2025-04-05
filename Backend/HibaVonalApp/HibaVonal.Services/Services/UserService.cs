using HibaVonal.DataContext;
using HibaVonal.DataContext.Dtos;
using HibaVonal.DataContext.Entities;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.Services.Services
{

    public interface IUserService
    {
        Task<IEnumerable<User>> List();
        Task<User> GetById(int id);
        Task<User> GetByEmail(string email);
        Task<User> Update(UserUpdateDto user);
        Task Delete(int id);
    }

    public class UserService : IUserService
    {
        SQL _context;
        public UserService(SQL context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> List()
        {
            List<User> users = await _context.User.ToListAsync();
            users.ForEach(u => u.Password = null);
            return users;
        }

        public async Task<User> GetById(int id)
        {
            User user = await _context.User.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                user.Password = null;
                return user;
            }
            else
            {
                throw new NotFoundException("User is not found!");
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            User user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                user.Password = null;
                return user;

            }
            else
            {
                throw new NotFoundException("User is not found!");
            }
        }

        public async Task<User> Update(UserUpdateDto user)
        {
            ObjectValidatorService<UserUpdateDto> objectValidatorService = new ObjectValidatorService<UserUpdateDto>(user);
            objectValidatorService.IsValid();
            User updatingUser = await _context.User.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (updatingUser == null)
            {
                throw new NotFoundException("User is not found!");
            }
            updatingUser.Name = user.Name;
            updatingUser.Email = user.Email;
            updatingUser.PhoneNumber = user.PhoneNumber;
            updatingUser.PersonalRoomId = user.PersonalRoomId;
            await _context.SaveChangesAsync();
            updatingUser.Password = null;
            return updatingUser;
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
