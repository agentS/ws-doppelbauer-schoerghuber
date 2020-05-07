using GraphOverflow.Dal;
using GraphOverflow.Domain;
using GraphOverflow.Dtos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace GraphOverflow.Services.Implementation
{
  public class TagService : ITagService
  {
    #region Members
    private readonly ISubject<TagDto> tagStream = new ReplaySubject<TagDto>(1);
    private readonly ISubject<List<TagDto>> allTagsStream = new ReplaySubject<List<TagDto>>(1);

    private readonly ITagDao tagDao;
    private int currentId;
    private IList<TagDto> tags;
    #endregion Members

    #region Construction
    public TagService(ITagDao tagDao)
    {
      AllTags = new ConcurrentStack<TagDto>();
      tags = new List<TagDto>
      {
        new TagDto { Id = 1, Name = "CSharp" },
        new TagDto { Id = 2, Name = "Java" },
        new TagDto { Id = 3, Name = "Async" },
        new TagDto { Id = 4, Name = "Go" }
      };
      currentId = 5;
      this.tagDao = tagDao;
    }
    #endregion Construction

    #region Properties
    public ConcurrentStack<TagDto> AllTags { get; private set; }
    #endregion Properties


    public TagDto AddTag(string tagName)
    {
      Tag tag = new Tag { Name = tagName };
      int result = tagDao.AddTag(tag);

      TagDto newTag = new TagDto { Id = currentId++, Name = tagName };
      tags.Add(newTag);

      AllTags.Push(newTag);
      tagStream.OnNext(newTag);
      
      return newTag;
    }

    public IEnumerable<TagDto> FindAllTags()
    {
      return tags;
    }

    public IEnumerable<TagDto> FindAllTagsByName(string tagName)
    {
      return tags.Where(tag => tag.Name.Contains(tagName));
    }

    public IObservable<TagDto> Tags()
    {
      return tagStream.AsObservable<TagDto>();
    }

    public async Task<IObservable<TagDto>> TaskAsync()
    {
      await Task.Delay(100);
      return Tags();
    }
  }
}
