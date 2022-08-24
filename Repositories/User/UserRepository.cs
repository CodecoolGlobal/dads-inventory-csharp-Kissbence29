using DadsInventory.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DadsInventory.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public Task<Models.User> AuthenticateAsync(string userName, string password) => Task.FromResult(_appDbContext.Users.FirstOrDefault(user => user.Username == userName && user.Password == password));
}

