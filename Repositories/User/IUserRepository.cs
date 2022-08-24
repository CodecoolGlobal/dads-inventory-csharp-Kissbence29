using System.Threading.Tasks;

namespace DadsInventory.Repositories.User;

public interface IUserRepository
{
    public Task<Models.User> AuthenticateAsync(string userName, string password);
}