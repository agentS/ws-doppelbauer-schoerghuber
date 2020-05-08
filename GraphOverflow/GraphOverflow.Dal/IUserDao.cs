using GraphOverflow.Domain;
using System.Threading.Tasks;

namespace GraphOverflow.Dal
{
  public interface IUserDao
  {
    Task<User> FindById(int id);
    Task<User> FindByName(string userName);
  }
}
