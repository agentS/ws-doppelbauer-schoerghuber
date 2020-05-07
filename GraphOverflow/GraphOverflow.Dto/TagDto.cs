using System;
using System.Collections.Generic;
using System.Text;

namespace GraphOverflow.Dtos
{
  /// <summary>
  /// tag that can be assigned to questions.
  /// </summary>
  public class TagDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<int> Questions { get; set; } // TODO: define question type
  }
}
