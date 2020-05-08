using System;

namespace GraphOverflow.Dtos
{
  public class QuestionDto : IPostDto
  {
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Title { get; set; }
    public long UpVotes { get; set; }
  }
}
