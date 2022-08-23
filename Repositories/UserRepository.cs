using DadsInventory.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DadsInventory.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<User> AuthenticateAsync(string userName, string password) => Task.FromResult(_appDbContext.Users.First(user => user.Username == userName && user.Password == password));
    }
}
