using GraphOverflow.Dal;
using GraphOverflow.Domain;
using GraphOverflow.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphOverflow.Services.Implementation
{
  public class CommentService : ICommentService
  {
    #region Members
    private readonly ICommentDao commentDao;
    #endregion Members

    #region Construction
    public CommentService(ICommentDao commentDao)
    {
      this.commentDao = commentDao;
    }
    #endregion Construction

    public async Task<IEnumerable<CommentDto>> FindCommentsForAnswer(AnswerDto answer)
    {
      return MapComments(await commentDao.FindCommentsByAnswerId(answer.Id));
    }

    public async Task<IEnumerable<CommentDto>> FindCommentsForQuestion(QuestionDto question)
    {
      return MapComments(await commentDao.FindCommentsByAnswerId(question.Id));
    }

    private IEnumerable<CommentDto> MapComments(IEnumerable<Comment> comments)
    {
      IList<CommentDto> commentDtos = new List<CommentDto>();
      foreach (var comment in comments)
      {
        commentDtos.Add(MapComment(comment));
      }
      return commentDtos;
    }

    private CommentDto MapComment(Comment comment)
    {
      var dto = new CommentDto
      {
        Id = comment.Id,
        Content = comment.Content,
        CreatedAt = comment.CreatedAt,
        AnswerId = comment.AnswerId,
        UserId = comment.UserId
      };
      return dto;
    }

    public async Task<CommentDto> CreateComment(string content, int answerId, int userId)
    {
      int commentId = await commentDao.CreateComment(content, answerId, userId);
      Comment comment = await commentDao.FindCommentsById(commentId);
      return MapComment(comment);
    }
  }
}
