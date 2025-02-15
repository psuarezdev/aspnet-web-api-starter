using AspNetWebApiStarter.Libs;
using AspNetWebApiStarter.Models;
using AspNetWebApiStarter.Models.DTOs.Auth;
using AspNetWebApiStarter.Models.DTOs.User;
using AutoMapper;

namespace AspNetWebApiStarter.Services
{
  public class AuthService : IServiceMarker
  {
    private readonly Utils _utils;
    private readonly IMapper _mapper;
    private readonly UserService _userService;

    public AuthService(UserService userService, Utils utils, IMapper mapper)
    {
      _utils = utils;
      _mapper = mapper;
      _userService = userService;
    }

    public async Task<UserDTO?> Login(LoginDTO loginDto)
    {
      var userFound = await _userService.FindByEmail(loginDto.Email);

      if (userFound == null) return null;

      var passwordMatch = _utils.VerifyPassword(loginDto.Password, userFound.Password);

      if (!passwordMatch) return null;

      return _mapper.Map<UserDTO>(userFound);
    }

    public async Task<UserDTO?> Register(CreateUserDTO registerDto)
    {
      var existingUser = await _userService.FindByEmail(registerDto.Email);

      if (existingUser != null) return null;

      var createdUser = await _userService.Create(registerDto);

      return createdUser != null ? _mapper.Map<UserDTO>(createdUser) : null;
    }
  }
}
