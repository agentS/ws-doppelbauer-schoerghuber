using System;

namespace GraphOverflow.Dtos
{
  public class AnswerDto : IPostDto
  {
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public long UpVotes { get; set; }
    public int QuestionId { get; set; }
    public int UserId { get; set; }
  }
}
