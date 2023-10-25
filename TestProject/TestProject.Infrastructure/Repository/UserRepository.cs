using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Entities;
using TestProject.Core.Interfaces;
using TestProject.Infrastructure.Data;
using TestProject.Infrastructure.Interfaces;

namespace TestProject.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TestContext _context;
        protected readonly DbSet<User> _entities;
        private readonly IPasswordService _passwordService;

        public UserRepository(TestContext context, IPasswordService passwordService)
        {
            _context = context;
            _entities = context.Set<User>();
            _passwordService = passwordService;
        }

        public async Task<User> GetLoginByCredentials(UserLogin login)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Email == login.Email);
        }
        public async Task RegisterUser(User user)
        {
            User register = new User
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
            };
            _context.Users.Add(register);
            await _context.SaveChangesAsync();
        }
    }
}
