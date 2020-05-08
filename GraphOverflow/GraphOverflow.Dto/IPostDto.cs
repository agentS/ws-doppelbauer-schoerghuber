using System;

namespace GraphOverflow.Dtos
{
  public interface IPostDto
  {
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UpVotes { get; set; }
  }
}
