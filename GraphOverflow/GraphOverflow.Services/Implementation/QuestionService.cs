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
    private readonly ITagDao tagDao;
    public QuestionService(IAnswerDao answerDao, ITagDao tagDao)
    {
      this.answerDao = answerDao;
      this.tagDao = tagDao;
    }

    public async Task<QuestionDto> FindQuestionById(int id)
    {
      return MapQuestion(await this.answerDao.FindQuestionById(id));
    }

    public async Task<QuestionDto> FindQuestionForAnswer(AnswerDto answer)
    {
      return MapQuestion(await answerDao.FindQuestionById(answer.QuestionId));
    }

    public async Task<QuestionDto> CreateQuestion(QuestionInputDto questionDto, int userId)
    {
      var question = new Answer()
      {
        Title =  questionDto.Title,
        Content = questionDto.Content,
      };
      int questionId = await answerDao.CreateQuestion(question, new User { Id = userId });

      foreach (string tag in questionDto.Tags)
      {
        Tag searchTag = await tagDao.FindByName(tag);
        if (searchTag != null)
        {
          await answerDao.AddTag(new Answer { Id = questionId }, searchTag);
        }
        else
        {
          int tagId = await tagDao.Add(new Tag { Name = tag });
          await answerDao.AddTag(new Answer { Id = questionId }, new Tag { Id = tagId });
        }
      }

      return MapQuestion(await answerDao.FindQuestionById(questionId));
    }

    public async Task<IEnumerable<QuestionDto>> FindLatestQuestions()
    {
      return MapQuestions(await answerDao.FindLatestQuestions());
    }

    public async Task<IEnumerable<QuestionDto>> FindQuestionsByTagId(int tagId)
    {
      return MapQuestions(await answerDao.FindQuestionsByTagId(tagId));
    }

    public async Task<QuestionDto> UpvoteQuestion(int questionId, int userId)
    {
      var question = await answerDao.FindQuestionById(questionId);
      if (question != null)
      {
        await answerDao.AddUpVote(new Answer { Id = question.Id }, new User { Id = userId });
        question = await answerDao.FindQuestionById(question.Id); // reload
      }
      return MapQuestion(question);
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
        Title = question.Title,
        UpVotes = question.UpVotes,
        UserId = question.UserId
      };
      return dto;
    }
  }
}
