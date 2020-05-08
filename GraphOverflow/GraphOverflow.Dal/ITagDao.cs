using GraphOverflow.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphOverflow.Dal
{
  public interface ITagDao : IDao<Tag, int>
  {
    Task<IEnumerable<Tag>> FindByPartialName(string tagName);
    Task<IEnumerable<Tag>> FindByAnswer(int answerId);
  }
}
