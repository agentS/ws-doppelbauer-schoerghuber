using GraphOverflow.Dtos;
using System.Collections.Generic;

namespace GraphOverflow.Services
{
  public interface ICommentService
  {
    IEnumerable<CommentDto> FindCommentsForAnswer(AnswerDto answer);
    IEnumerable<CommentDto> FindCommentsForQuestion(QuestionDto question);
  }
}
