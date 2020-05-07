using System;
using System.Collections.Generic;
using System.Text;

namespace GraphOverflow.Domain
{
  public class Comment
  {
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int AnswerId { get; set; }
  }
}
