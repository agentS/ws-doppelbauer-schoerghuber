using System.Collections.Generic;

namespace GraphOverflow.Dtos.Input
{
    public class QuestionInputDto
    {
        public string Title { get; set; }
        
        public string Content { get; set; }

        public IList<string> Tags { get; set; }
  }
}