using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Entities;

namespace TestProject.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetLoginByCredentials(UserLogin login);
        Task RegisterUser(User user);
    }
}
