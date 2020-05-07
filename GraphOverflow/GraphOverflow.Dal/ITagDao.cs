using GraphOverflow.Domain;
using System.Collections.Generic;

namespace GraphOverflow.Dal
{
  public interface ITagDao : IDao<Tag, int>
  {
    IEnumerable<Tag> FindByName(string tagName);
    IEnumerable<Tag> FindByAnswer(Answer answer);
  }
}
