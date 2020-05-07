using GraphOverflow.Dtos;
using System.Collections.Generic;

namespace GraphOverflow.Services
{
  public interface IAnswerService
  {
    IEnumerable<AnswerDto> FindAnswersForQuestion(QuestionDto question);
    AnswerDto FindAnswerForComment(CommentDto comment);
  }
}
