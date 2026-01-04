using ProductManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task AddAsync(User user);
        Task<bool> ExistsAsync(string username);
    }
}
