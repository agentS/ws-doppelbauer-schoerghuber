using System;
using System.Collections.Generic;

namespace GraphOverflow.Dtos
{
  public class QuestionDto : IPostDto
  {
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Title { get; set; }
    public long UpVotes { get; set; }
    public int UserId { get; set; }
    public IList<AnswerDto> Answers { get; set; }

    public QuestionDto()
    {
      this.Answers = new List<AnswerDto>();
    }
  }
}
