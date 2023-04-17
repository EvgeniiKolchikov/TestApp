using Microsoft.EntityFrameworkCore;
using TestApplication.Contexts;
using TestApplication.Exceptions;
using TestApplication.Interfaces;
using TestApplication.Models;

namespace TestApplication.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task AddUser(User user)
    {
       await _context.AddAsync(user);
       await _context.SaveChangesAsync();
    }

    public async Task<User> GetUser(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id && u.IsDeleted == false);
        if (user == null)
        {
            throw new CustomException("User not found");
        }

        return user;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _context.Users.Where(u => u.IsDeleted == false);
    }
    
    public async Task EditUser(User user)
    {
        var userFromDb = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id && u.IsDeleted == false);
        if (userFromDb == null) throw new CustomException("User not found");
        userFromDb.Name = user.Name;
        userFromDb.Surname = user.Surname;
        userFromDb.PatronymicName = user.PatronymicName;
        userFromDb.ActiveDirectoryLogin = user.ActiveDirectoryLogin;
        _context.Users.Update(userFromDb);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUser(User user)
    {
        var userFromDb = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id && u.IsDeleted == false);
        if (userFromDb == null) throw new CustomException("User not found");
        userFromDb.IsDeleted = true;
        _context.Users.Update(userFromDb);
        await _context.SaveChangesAsync();
    }
}