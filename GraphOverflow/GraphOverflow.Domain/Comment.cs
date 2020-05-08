using System;

namespace GraphOverflow.Domain
{
  public class Comment
  {
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int AnswerId { get; set; }
    public int UserId { get; set; }
  }
}
