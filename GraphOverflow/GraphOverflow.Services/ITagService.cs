using GraphOverflow.Dtos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphOverflow.Services
{
  public interface ITagService
  {
    IEnumerable<TagDto> FindAllTags();
    IEnumerable<TagDto> FindAllTagsByName(string tagName);
    IEnumerable<TagDto> FindByQuestion(QuestionDto question);
    ConcurrentStack<TagDto> AllTags { get; }
    IObservable<TagDto> Tags();
    TagDto AddTag(string tagName);
    Task<IObservable<TagDto>> TaskAsync();
  }
}
