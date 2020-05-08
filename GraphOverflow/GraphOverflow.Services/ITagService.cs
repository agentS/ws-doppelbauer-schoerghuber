using GraphOverflow.Dtos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphOverflow.Services
{
  public interface ITagService
  {
    Task<IEnumerable<TagDto>> FindAllTags();
    Task<IEnumerable<TagDto>> FindAllTagsByName(string tagName);
    Task<IEnumerable<TagDto>> FindByQuestion(QuestionDto question);
    ConcurrentStack<TagDto> AllTags { get; }
    IObservable<TagDto> Tags();
    Task<TagDto> AddTag(string tagName);
    Task<IObservable<TagDto>> TaskAsync();
  }
}
