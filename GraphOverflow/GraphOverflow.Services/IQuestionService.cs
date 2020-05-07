using GraphOverflow.Dtos;
using System.Collections.Generic;

namespace GraphOverflow.Services
{
  public interface IQuestionService
  {
    IEnumerable<QuestionDto> FindQuestionsByTagId(int tagId);
    QuestionDto FindQuestionForAnswer(AnswerDto answer);
  }
}
