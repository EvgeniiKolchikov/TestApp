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
    private readonly IProvider _provider;
    public UserController(IUserRepository userRepository, IProvider provider)
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
            if (!_provider.UserExist()) return NotFound();
            await _userRepository.AddUser(user);
            return Ok();
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
            if (!_provider.UserExist()) return NotFound();
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
            if (!_provider.UserExist()) return NotFound();
            await _userRepository.DeleteUser(user);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}