using HibaVonal.DataContext;
using HibaVonal.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Services.Services
{

    public interface IUserService
    {
        Task<IEnumerable<User>> List();
        Task<User> Get(int id);
        Task<User> Update(User user);
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

        public async Task<User> Get(int id)
        {
            User user = await _context.User.FindAsync(id);
            user.Password = null;
            return user;
        }

        public async Task<User> Update(User user)
        {
            //ellenőrzések 
            _context.User.Update(user);
            await _context.SaveChangesAsync();
            user.Password = null;
            return user;
        }

        public async Task Delete(int id)
        {
            User user = await _context.User.FindAsync(id);
            user.IsDeleted = true;
            await _context.SaveChangesAsync();
        }


    }
}
