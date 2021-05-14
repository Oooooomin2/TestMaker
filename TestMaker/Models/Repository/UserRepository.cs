using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Data;
using TestMaker.Models.Interface;

namespace TestMaker.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TestMakerContext _context;

        public UserRepository(TestMakerContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
                await _context.Users.AddAsync(model);
                _context.SaveChanges();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var target = await _context.Users.FindAsync(id);
            _context.Users.Remove(target);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetContent(int? id)
        {
            return await _context.Users.SingleOrDefaultAsync(m => m.UserId == id);
        }

        public User FindUser(int? id)
        {
            return _context.Users.Find(id);
        }

        public bool LoginIdExists(User user)
        {
            return _context.Users.Where(o => o.LoginId == user.LoginId).Any();
        }

        public async Task UpdateAsync(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
                _context.Users.Update(model);
                await _context.SaveChangesAsync();
            }
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
