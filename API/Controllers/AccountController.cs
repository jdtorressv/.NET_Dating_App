﻿using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
public class AccountController : BaseApiController
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    public AccountController(DataContext context, ITokenService tokenService, IMapper mapper)
    {
        _context = context;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    [HttpPost("register")] // Allow new user to register (/api/account/register, with method POST)
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
    {
        if (await UserExists(registerDTO.Username)) return BadRequest("Username already in use");

        var user = _mapper.Map<AppUser>(registerDTO);

        user.UserName = registerDTO.Username.ToLower();

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserDTO
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user),
            KnownAs = user.KnownAs,
            Gender = user.Gender
        };
    }

    [HttpPost("login")] // Let user login (/api/account/login, with method POST)

    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
    {
        var user = await _context.Users
        .Include(p => p.Photos)
        .FirstOrDefaultAsync(x => x.UserName == loginDTO.Username);


        if (user == null) return Unauthorized("Invalid Username");

        return new UserDTO
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user),
            PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
            KnownAs = user.KnownAs,
            Gender = user.Gender
        };
    }

    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
    }
}
