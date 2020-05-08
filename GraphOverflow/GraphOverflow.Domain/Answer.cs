using System;

namespace GraphOverflow.Domain
{
  public class Answer
  {
    public int Id { get; set; }
    public string Content { get; set; }
    public string Title { get; set; }
    public long UpVotes { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? QuestionId { get; set; }
    public int UserId { get; set; }
  }
}
