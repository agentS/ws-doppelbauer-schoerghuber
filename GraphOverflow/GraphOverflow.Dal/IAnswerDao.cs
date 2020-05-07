using GraphOverflow.Domain;
using System.Collections.Generic;

namespace GraphOverflow.Dal
{
  public interface IAnswerDao
  {
    IEnumerable<Answer> FindQuestionsByTagId(int tagId);
  }
}
