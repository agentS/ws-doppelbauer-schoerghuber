using GraphOverflow.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphOverflow.Dal
{
  public interface IAnswerDao
  {
    Task<IEnumerable<Answer>> FindLatestQuestions();
    Task<IEnumerable<Answer>> FindQuestionsByTagId(int tagId);
    Task<IEnumerable<Answer>> FindAnswersByQuestionId(int questionId);
    Task<Answer> FindAnswerById(int answerId);
    Task<Answer> FindQuestionById(int questionId);

    Task<int> CreateQuestion(Answer question);
  }
}
