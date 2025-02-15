using AspNetWebApiStarter.Models.DTOs.User;
using AutoMapper;

namespace AspNetWebApiStarter.Models.Mappers
{
  public class UserMapper : Profile
  {
    public UserMapper()
    {
      CreateMap<User, UserDTO>();
      CreateMap<CreateUserDTO, User>();
    }
  }
}
