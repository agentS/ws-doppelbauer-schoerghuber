using GraphOverflow.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphOverflow.Dtos.Input;

namespace GraphOverflow.Services
{
  public interface IQuestionService
  {
    IEnumerable<QuestionDto> FindQuestionsByTagId(int tagId);
    QuestionDto FindQuestionForAnswer(AnswerDto answer);

    Task<QuestionDto> CreateQuestion(QuestionInputDto question);
  }
}
