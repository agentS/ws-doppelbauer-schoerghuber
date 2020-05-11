using GraphOverflow.Dtos;
using GraphOverflow.Dtos.Input;
using System.Threading.Tasks;

namespace GraphOverflow.Services
{
  public interface IAuthenticationService
  {
    Task<AuthPayloadDto> Authenticate(UserLoginInputDto userLogin);
    Task<UserDto> GetAuthenticatedUser(string token);
    bool ValidateToken(string token);

  }
}
