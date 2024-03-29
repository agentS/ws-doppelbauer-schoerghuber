﻿using System;
using GraphOverflow.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphOverflow.Services
{
  public interface IAnswerService
  {
    Task<IEnumerable<AnswerDto>> FindAnswersForQuestion(QuestionDto question);
    Task<ILookup<QuestionDto, AnswerDto>> FindAnswersForQuestions(IEnumerable<QuestionDto> questions);
    Task<AnswerDto> FindAnswerForComment(CommentDto comment);
    Task<AnswerDto> UpvoteAnswer(int answerId, int userId);
    Task<AnswerDto> CreateAnswer(string content, int questionId, int userId);
    IObservable<AnswerDto> Answers();
  }
}
