using GraphOverflow.Dal;
using GraphOverflow.Domain;
using GraphOverflow.Dtos;
using System.Collections.Generic;

namespace GraphOverflow.Services.Implementation
{
  public class QuestionService : IQuestionService
  {
    private readonly IAnswerDao answerDao;
    public QuestionService(IAnswerDao answerDao)
    {
      this.answerDao = answerDao;
    }
    public IEnumerable<QuestionDto> FindQuestionsByTagId(int tagId)
    {
      return MapQuestions(answerDao.FindQuestionsByTagId(tagId));
    }

    private IEnumerable<QuestionDto> MapQuestions(IEnumerable<Answer> questions)
    {
      IList<QuestionDto> questionDtos = new List<QuestionDto>();
      foreach (var question in questions)
      {
        questionDtos.Add(MapTag(question));
      }
      return questionDtos;
    }

    private QuestionDto MapTag(Answer question)
    {
      var dto = new QuestionDto
      {
        Id = question.Id,
        Content = question.Content,
        CreatedAt = question.CreatedAt,
        Title = question.Title
      };
      return dto;
    }
  }
}
