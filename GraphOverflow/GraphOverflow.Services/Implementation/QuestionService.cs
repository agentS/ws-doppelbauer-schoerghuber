using GraphOverflow.Dal;
using GraphOverflow.Domain;
using GraphOverflow.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphOverflow.Dtos.Input;

namespace GraphOverflow.Services.Implementation
{
  public class QuestionService : IQuestionService
  {
    private readonly IAnswerDao answerDao;
    public QuestionService(IAnswerDao answerDao)
    {
      this.answerDao = answerDao;
    }

    public QuestionDto FindQuestionForAnswer(AnswerDto answer)
    {
      return MapQuestion(answerDao.FindQuestionById(answer.QuestionId));
    }

    public async Task<QuestionDto> CreateQuestion(QuestionInputDto questionDto)
    {
      var question = new Answer()
      {
        Title =  questionDto.Title,
        Content = questionDto.Content,
      };
      int questionId = await answerDao.CreateQuestion(question);
      return MapQuestion(answerDao.FindQuestionById(questionId));
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
        questionDtos.Add(MapQuestion(question));
      }
      return questionDtos;
    }

    private QuestionDto MapQuestion(Answer question)
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
