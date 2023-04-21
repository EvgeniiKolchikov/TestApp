using Microsoft.EntityFrameworkCore;
using TestApplication.Contexts;
using TestApplication.Exceptions;
using TestApplication.Interfaces;
using TestApplication.Models;
using TestApplication.Models.ViewModels;

namespace TestApplication.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task AddUser(UserViewModel userViewModel)
    {
        var user = new User
        {
            Id = userViewModel.Id,
            ActiveDirectoryLogin = userViewModel.ActiveDirectoryLogin,
            Name = userViewModel.Name,
            Surname = userViewModel.Surname,
            PatronymicName = userViewModel.PatronymicName,
            IsDeleted = false
        };
       await _context.AddAsync(user);
       await _context.SaveChangesAsync();
    }

    public async Task<UserViewModel> GetUser(int id)
    {
        
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id && u.IsDeleted == false);
        if (user == null)
        {
            throw new CustomException("User not found");
        }

        var userViewModel = new UserViewModel
        {
            Id = user.Id,
            ActiveDirectoryLogin = user.ActiveDirectoryLogin,
            Name = user.Name,
            Surname = user.Surname,
            PatronymicName = user.PatronymicName
        };
        return userViewModel;
    }

    public IQueryable<UserViewModel> GetAllUsers()
    {
        return _context.Users.AsNoTracking().Where(u => u.IsDeleted == false)
            .Select(u => new UserViewModel
            {
                Id = u.Id,
                ActiveDirectoryLogin = u.ActiveDirectoryLogin,
                Name = u.Name,
                Surname = u.Surname,
                PatronymicName = u.PatronymicName
            });
    }
    
    public async Task EditUser(UserViewModel user)
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

    public async Task DeleteUser(UserViewModel user)
    {
        var userFromDb = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id && u.IsDeleted == false);
        if (userFromDb == null) throw new CustomException("User not found");
        userFromDb.IsDeleted = true;
        _context.Users.Update(userFromDb);
        await _context.SaveChangesAsync();
    }
}