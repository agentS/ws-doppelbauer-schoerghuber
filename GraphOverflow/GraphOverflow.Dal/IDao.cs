using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphOverflow.Dal
{
  public interface IDao<Type, ID>
  {
    Task<ID> Add(Type tag);
    Task<Type> FindById(ID id);
    Task<IEnumerable<Type>> FindAll();
  }
}
