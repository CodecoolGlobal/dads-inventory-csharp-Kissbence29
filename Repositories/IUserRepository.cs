using DadsInventory.Models;
using System.Threading.Tasks;

namespace DadsInventory.Repositories;

public interface IUserRepository
{
    public Task<User> AuthenticateAsync(string userName, string password);
}