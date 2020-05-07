using GraphOverflow.Domain;
using System.Collections.Generic;

namespace GraphOverflow.Dal
{
  public interface IAnswerDao
  {
    IEnumerable<Answer> FindQuestionsByTagId(int tagId);
    IEnumerable<Answer> FindAnswersByQuestionId(int questionId);
    Answer FindAnswerById(int answerId);
    Answer FindQuestionById(int questionId);
  }
}
