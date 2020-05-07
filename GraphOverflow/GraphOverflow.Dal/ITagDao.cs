using GraphOverflow.Domain;
using System.Collections.Generic;

namespace GraphOverflow.Dal
{
  public interface ITagDao : IDao<Tag, int>
  {
    IEnumerable<Tag> FindByPartialName(string tagName);
    IEnumerable<Tag> FindByAnswer(int answerId);
  }
}
