using System.Collections.Generic;

namespace GraphOverflow.Dal
{
  public interface IDao<Type, ID>
  {
    ID AddTag(Type tag);
    Type FindById(ID id);
    IEnumerable<Type> FindAll();
  }
}
