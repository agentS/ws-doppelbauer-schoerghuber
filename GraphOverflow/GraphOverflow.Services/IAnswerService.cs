using GraphOverflow.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphOverflow.Services
{
  public interface IAnswerService
  {
    Task<IEnumerable<AnswerDto>> FindAnswersForQuestion(QuestionDto question);
    Task<AnswerDto> FindAnswerForComment(CommentDto comment);
    Task<AnswerDto> UpvoatAnswer(int answerId, int userId);
  }
}
