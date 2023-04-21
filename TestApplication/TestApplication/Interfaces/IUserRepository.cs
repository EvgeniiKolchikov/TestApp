using TestApplication.Models;
using TestApplication.Models.ViewModels;

namespace TestApplication.Interfaces;

public interface IUserRepository
{
    Task AddUser(UserViewModel user);
    Task<UserViewModel> GetUser(int id);
    IQueryable<UserViewModel> GetAllUsers();
    Task EditUser(UserViewModel user);
    Task DeleteUser(UserViewModel user);
}