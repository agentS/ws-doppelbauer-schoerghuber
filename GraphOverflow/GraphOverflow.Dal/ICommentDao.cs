using GraphOverflow.Domain;
using System.Collections.Generic;

namespace GraphOverflow.Dal
{
  public interface ICommentDao
  {
    IEnumerable<Comment> FindCommentsByAnswerId(int answerId);
  }
}
