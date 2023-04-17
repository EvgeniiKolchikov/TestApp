using TestApplication.Models;

namespace TestApplication.Interfaces;

public interface IUserRepository
{
    Task AddUser(User user);
    Task<User> GetUser(int id);
    IEnumerable<User> GetAllUsers();
    Task EditUser(User user);
    Task DeleteUser(User user);
}