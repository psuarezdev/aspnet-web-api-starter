using AspNetWebApiStarter.Libs;
using AspNetWebApiStarter.Models;
using AspNetWebApiStarter.Models.DTOs.User;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AspNetWebApiStarter.Services
{
  public class UserService : IServiceMarker
  {
    private readonly Utils _utils;
    private readonly IMapper _mapper;
    private readonly DatabaseContext _context;

    public UserService(DatabaseContext context, Utils utils, IMapper mapper)
    {
      _utils = utils;
      _mapper = mapper;
      _context = context;
    }

    public async Task<UserDTO?> FindById(int id)
    {
      var user = await _context.Users.FindAsync(id);
      return user != null ? _mapper.Map<UserDTO>(user) : null;
    }

    public async Task<User?> FindByEmail(string Email)
    {
      return await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);
    }

    public async Task<UserDTO?> Create(CreateUserDTO createUserDTO)
    {
      try
      {
        var user = _mapper.Map<User>(createUserDTO);
        user.Password = _utils.HashPassword(createUserDTO.Password);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return _mapper.Map<UserDTO>(user);
      }
      catch (Exception ex)
      {
        return null;
      }
    }
  }
}
