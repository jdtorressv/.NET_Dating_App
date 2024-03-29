using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
public class AccountController : BaseApiController
{
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper)
    {
        _tokenService = tokenService;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpPost("register")] // Allow new user to register (/api/account/register, with method POST)
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
    {
        if (await UserExists(registerDTO.Username)) return BadRequest("Username already in use");

        var user = _mapper.Map<AppUser>(registerDTO);

        user.UserName = registerDTO.Username.ToLower();

        var result = await _userManager.CreateAsync(user, registerDTO.Password);

        if (!result.Succeeded) return BadRequest(result.Errors);

        var roleResult = await _userManager.AddToRoleAsync(user, "Member");

        if (!roleResult.Succeeded) return BadRequest(result.Errors);

        return new UserDTO
        {
            Username = user.UserName,
            Token = await _tokenService.CreateToken(user),
            KnownAs = user.KnownAs,
            Gender = user.Gender
        };
    }

    [HttpPost("login")] // Let user login (/api/account/login, with method POST)

    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
    {
        var user = await _userManager.Users
        .Include(p => p.Photos)
        .FirstOrDefaultAsync(x => x.UserName == loginDTO.Username);


        if (user == null) return Unauthorized("Invalid Username");

        var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

        if (!result) return Unauthorized("Invalid password");

        return new UserDTO
        {
            Username = user.UserName,
            Token = await _tokenService.CreateToken(user),
            PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
            KnownAs = user.KnownAs,
            Gender = user.Gender
        };
    }

    private async Task<bool> UserExists(string username)
    {
        return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
    }
}
