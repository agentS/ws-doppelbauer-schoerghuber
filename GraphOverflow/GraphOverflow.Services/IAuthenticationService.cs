using GraphOverflow.Dtos;
using GraphOverflow.Dtos.Input;

namespace GraphOverflow.Services
{
  public interface IAuthenticationService
  {
    AuthPayloadDto Authenticate(UserLoginInputDto userLogin);
    UserDto GetAuthenticatedUser(string token);
    bool ValidateToken(string token);

  }
}
