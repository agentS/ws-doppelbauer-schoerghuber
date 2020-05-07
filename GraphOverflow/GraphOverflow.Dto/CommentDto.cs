using System;

namespace GraphOverflow.Dtos
{
  public class CommentDto : IPostDto
  {
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int AnswerId { get; set; }
  }
}
