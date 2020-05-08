using GraphOverflow.Dtos;
using System.Threading.Tasks;

namespace GraphOverflow.Services
{
  public interface IUserService
  {
    Task<UserDto> FindUserById(int id);
  }
}
