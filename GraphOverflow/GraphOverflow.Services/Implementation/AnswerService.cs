using GraphOverflow.Dal;
using GraphOverflow.Domain;
using GraphOverflow.Dtos;
using System.Collections.Generic;

namespace GraphOverflow.Services.Implementation
{
  public class AnswerService : IAnswerService
  {
    #region Members
    private readonly IAnswerDao answerDao;
    #endregion Members

    #region Construction
    public AnswerService(IAnswerDao answerDao)
    {
      this.answerDao = answerDao;
    }
    #endregion Construction


    public IEnumerable<AnswerDto> FindAnswersForQuestion(QuestionDto question)
    {
      return MapAnswers(answerDao.FindAnswersByQuestionId(question.Id));
    }

    public AnswerDto FindAnswerForComment(CommentDto comment)
    {
      return MapAnswer(answerDao.FindAnswerById(comment.AnswerId));
    }

    private IEnumerable<AnswerDto> MapAnswers(IEnumerable<Answer> answers)
    {
      IList<AnswerDto> questionDtos = new List<AnswerDto>();
      foreach (var answer in answers)
      {
        questionDtos.Add(MapAnswer(answer));
      }
      return questionDtos;
    }

    private AnswerDto MapAnswer(Answer answer)
    {
      var dto = new AnswerDto
      {
        Id = answer.Id,
        Content = answer.Content,
        CreatedAt = answer.CreatedAt,
        UpVotes = answer.UpVoats,
        QuestionId = answer.QuestionId.Value
      };
      return dto;
    }
  }
}
