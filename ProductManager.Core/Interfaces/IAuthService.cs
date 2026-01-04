using ProductManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Core.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(User user, string password);
        Task<string> LoginAsync(string username, string password);
    }
}
