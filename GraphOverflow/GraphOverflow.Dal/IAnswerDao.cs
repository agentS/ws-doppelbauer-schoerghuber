using GraphOverflow.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphOverflow.Dal
{
  public interface IAnswerDao
  {
    Task<IEnumerable<Answer>> FindLatestQuestions();
    Task<IEnumerable<Answer>> FindLatestQuestionsByUserId(int userId);
    Task<IEnumerable<Answer>> FindAnswersByIds(IEnumerable<int> questionIds);
    Task<IEnumerable<Answer>> FindQuestionsByTagId(int tagId);
    Task<IEnumerable<Answer>> FindAnswersByQuestionId(int questionId);
    Task<Answer> FindAnswerById(int answerId);
    Task<Answer> FindQuestionById(int questionId);
    Task<IEnumerable<Answer>> FindQuestionsByTagName(string tagName);
    Task<IEnumerable<UpVoteUser>> FindUpVoteUsersForPost(int postId);
    Task<int> CreateQuestion(Answer question, User user);
    Task<bool> Update(Answer question);
    Task<bool> AddUpVote(Answer question, User user);
    Task<int> CreateAnswer(string content, int questionId, int userId);
    Task<bool> AddTag(Answer question, Tag tag);
  }
}
