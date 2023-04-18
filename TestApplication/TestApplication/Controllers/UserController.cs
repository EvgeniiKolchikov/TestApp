using Microsoft.AspNetCore.Mvc;
using TestApplication.Exceptions;
using TestApplication.Interfaces;
using TestApplication.Models;

namespace TestApplication.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpGet("all")]
    public  IActionResult AllUsers()
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
            HttpContext.Response.StatusCode = 200;
            return Json(user);
        }
        catch (CustomException)
        {
            HttpContext.Response.StatusCode = 204;
            return NotFound();
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody]User user)
    {
        try
        {
            //  При создании, либо изменении записи справочника сервис должен выполнять проверку наличия информации о пользователе в домене Active Directory
            await _userRepository.AddUser(user);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    [HttpPost("edit")]
    public async Task<IActionResult> EditUser([FromBody]User user)
    {
        try
        {
            //  При создании, либо изменении записи справочника сервис должен выполнять проверку наличия информации о пользователе в домене Active Directory
            await _userRepository.EditUser(user);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    [HttpPost("delete")]
    public async Task<IActionResult> DeleteUser([FromBody]User user)
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