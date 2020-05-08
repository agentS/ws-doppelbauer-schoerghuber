﻿using GraphOverflow.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphOverflow.Dtos.Input;

namespace GraphOverflow.Services
{
  public interface IQuestionService
  {
    Task<IEnumerable<QuestionDto>> FindQuestionsByTagId(int tagId);
    Task<QuestionDto> FindQuestionForAnswer(AnswerDto answer);
    Task<QuestionDto> CreateQuestion(QuestionInputDto question);
    Task<QuestionDto> UpvoatQuestion(int questionId, int userId);
  }
}
