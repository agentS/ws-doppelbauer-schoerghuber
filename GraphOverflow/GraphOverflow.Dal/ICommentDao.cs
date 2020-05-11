using GraphOverflow.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphOverflow.Dal
{
  public interface ICommentDao
  {
    Task<IEnumerable<Comment>> FindCommentsByAnswerId(int answerId);
    Task<int> CreateComment(string content, int answerId, int userId);
    Task<Comment> FindCommentsById(int commentId);
  }
}
