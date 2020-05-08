using GraphOverflow.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphOverflow.Dal
{
  public interface ICommentDao
  {
    Task<IEnumerable<Comment>> FindCommentsByAnswerId(int answerId);
  }
}
