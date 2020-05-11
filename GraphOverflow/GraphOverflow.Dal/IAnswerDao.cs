﻿using GraphOverflow.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphOverflow.Dal
{
  public interface IAnswerDao
  {
    Task<IEnumerable<Answer>> FindLatestQuestions();
    Task<IEnumerable<Answer>> FindQuestionsByTagId(int tagId);
    Task<IEnumerable<Answer>> FindAnswersByQuestionId(int questionId);
    Task<Answer> FindAnswerById(int answerId);
    Task<Answer> FindQuestionById(int questionId);
    Task<IEnumerable<UpVoteUser>> FindUpVoteUsersForPost(int postId);
    Task<int> CreateQuestion(Answer question, User user);
    Task<bool> Update(Answer question);
    Task<bool> AddUpVote(Answer question, User user);
    Task<int> CreateAnswer(string content, int questionId, int userId);
    Task<bool> AddTag(Answer question, Tag tag);
  }
}
