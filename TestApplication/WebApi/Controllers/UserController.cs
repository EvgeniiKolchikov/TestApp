using Microsoft.AspNetCore.Mvc;
using WebApi.Exceptions;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Models.ViewModels;

namespace WebApi.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IActiveDirectoryProvider _provider;
    private readonly Client _client;
    public UserController(IUserRepository userRepository, IActiveDirectoryProvider provider)
    {
        _userRepository = userRepository;
        _provider = provider;
    }
    
    [HttpGet]
    public IActionResult AllUsers()
    {
        try
        {
            var users = _userRepository.GetAllUsers().ToList();
            return Json(users);
        }
        catch (CustomException)
        {
            return NotFound();
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUser(int id)
    {
        try
        {
            var user = await _userRepository.GetUser(id);
            return Json(user);
        }
        catch (CustomException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody]UserViewModel user)
    {
        try
        {
            if (_provider.UserExist())
            {
                await _userRepository.AddUser(user);
                return Ok();
            }
            return NotFound();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> EditUser([FromBody]UserViewModel userViewModel)
    {
        try
        {
            //  При создании, либо изменении записи справочника сервис должен выполнять проверку наличия информации о пользователе в домене Active Directory
            await _userRepository.EditUser(userViewModel);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteUser([FromBody]UserViewModel user)
    {
        try
        {
            //  При создании, либо изменении записи справочника сервис должен выполнять проверку наличия информации о пользователе в домене Active Directory
            await _userRepository.DeleteUser(user);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}