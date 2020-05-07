using System;

namespace GraphOverflow.Dtos
{
  public class AnswerDto : IPostDto
  {
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UpVoats { get; set; }
    public int QuestionId { get; set; }
  }
}
