using System;
using GraphOverflow.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphOverflow.Services
{
  public interface ICommentService
  {
    Task<IEnumerable<CommentDto>> FindCommentsForAnswer(AnswerDto answer);
    Task<IEnumerable<CommentDto>> FindCommentsForQuestion(QuestionDto question);
    Task<CommentDto> CreateComment(string content, int answerId, int id);
    IObservable<CommentDto> Comments();
  }
}
