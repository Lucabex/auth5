using Microsoft.AspNetCore.Mvc;
using auth5.Data;
using auth5.Models;
using auth5.DTO;
using Microsoft.EntityFrameworkCore;

namespace auth5.Controllers;

[ApiController]
[Route("auth")]
public class AuthControllers : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthControllers(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("reg")]
    public async Task<IActionResult> Reg(RegDto dto)
    {
        if(await _context.User.AnyAsync(u => u.Name.ToLower() == dto.Name.ToLower()))
        {
            return BadRequest("usernmae already in use");
        }
        var user =new User
        {
            Name = dto.Name,
            HashedPassword =BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };
        return Ok("user regisetered");
    }
    [HttpGet("log")]
    public async Task<IActionResult> Log(LogDto dto)
    {
        var user = await _context.User.FirstOrDefaultAsync(u => u.Name.ToLower() == dto.Name.ToLower());

        if(user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.HashedPassword))
        {
            return BadRequest("Invalid Username or Password");
        }

        return Ok("user logged");
    }

}