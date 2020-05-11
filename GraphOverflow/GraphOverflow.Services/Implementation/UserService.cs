using GraphOverflow.Dal;
using GraphOverflow.Domain;
using GraphOverflow.Dtos;
using System.Threading.Tasks;

namespace GraphOverflow.Services.Implementation
{
  public class UserService : IUserService
  {
    #region Members
    private IUserDao userDao;
    #endregion Members

    #region Construction
    public UserService(IUserDao userDao)
    {
      this.userDao = userDao;
    }
    #endregion Construction

    public async Task<UserDto> FindUserById(int id)
    {
      return MapUser(await userDao.FindById(id));
    }

    private UserDto MapUser(User user)
    {
      return new UserDto
      {
        Id = user.Id,
        Name = user.Name
      };
    }
  }
}
