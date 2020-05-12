using System;
using GraphOverflow.Dal;
using GraphOverflow.Domain;
using GraphOverflow.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace GraphOverflow.Services.Implementation
{
  public class AnswerService : IAnswerService
  {
    #region Members
    private readonly IAnswerDao answerDao;
    private readonly ISubject<AnswerDto> answerStream;
    
    #endregion Members

    #region Construction
    public AnswerService(IAnswerDao answerDao)
    {
      this.answerDao = answerDao;
      this.answerStream = new Subject<AnswerDto>();
    }
    #endregion Construction


    public async Task<IEnumerable<AnswerDto>> FindAnswersForQuestion(QuestionDto question)
    {
      return MapAnswers(await answerDao.FindAnswersByQuestionId(question.Id));
    }

    public async Task<ILookup<QuestionDto, AnswerDto>> FindAnswersForQuestions(IEnumerable<QuestionDto> questions)
    {
      IEnumerable<Answer> answers = await this.answerDao.FindAnswersByIds(
        questions.Select(question => question.Id)
      );
      return answers
        .Select(answer => MapAnswer(answer))
        .ToLookup(answer => questions.First(question => question.Id == answer.QuestionId));
    }

    public async Task<AnswerDto> FindAnswerForComment(CommentDto comment)
    {
      return MapAnswer(await answerDao.FindAnswerById(comment.AnswerId));
    }

    public async Task<AnswerDto> UpvoteAnswer(int answerId, int userId)
    {
      var answer = await answerDao.FindAnswerById(answerId);
      if (answer != null)
      {
        await answerDao.AddUpVote(new Answer { Id = answer.Id }, new User { Id = userId });
        answer = await answerDao.FindQuestionById(answer.Id); // reload
      }
      return MapAnswer(answer);
    }

    public async Task<AnswerDto> CreateAnswer(string content, int questionId, int userId)
    {
      int id = await answerDao.CreateAnswer(content, questionId, userId);
      Answer answer = await answerDao.FindAnswerById(id);
      var dto = MapAnswer(answer);
      answerStream.OnNext(dto);
      return dto;
    }

    public IObservable<AnswerDto> Answers()
    {
      return answerStream.AsObservable();
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
        UpVotes = answer.UpVotes,
        QuestionId = answer.QuestionId.Value,
        UserId = answer.UserId
      };
      return dto;
    }
  }
}
