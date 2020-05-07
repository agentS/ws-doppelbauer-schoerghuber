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
      int tagId = tagDao.Add(tag);
      Tag newTag = tagDao.FindById(tagId);
      if (newTag != null)
      {
        TagDto dto = MapTag(newTag);
        AllTags.Push(dto);
        tagStream.OnNext(dto);
        return dto;
      }
      else
      {
        throw new ArgumentException();
      }

      //TagDto newTag = new TagDto { Id = currentId++, Name = tagName };
      //tags.Add(newTag);

      //AllTags.Push(newTag);
      //tagStream.OnNext(newTag);
      
    }

    public IEnumerable<TagDto> FindAllTags()
    {
      //return tags;
      var tags = tagDao.FindAll();
      IEnumerable<TagDto> tagDtos = MapTags(tags);
      return tagDtos;
    }

    public IEnumerable<TagDto> FindAllTagsByName(string tagName)
    {
      //return tags.Where(tag => tag.Name.Contains(tagName));
      return MapTags(tagDao.FindByPartialName(tagName));
    }

    public IEnumerable<TagDto> FindByQuestion(QuestionDto question)
    {
      return MapTags(tagDao.FindByAnswer(question.Id));
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

    private IEnumerable<TagDto> MapTags(IEnumerable<Tag> tags)
    {
      IList<TagDto> tagDtos = new List<TagDto>();
      foreach (var tag in tags)
      {
        tagDtos.Add(MapTag(tag));
      }

      return tagDtos;
    }

    private static TagDto MapTag(Tag tag)
    {
      return new TagDto { Id = tag.Id, Name = tag.Name };
    }
  }
}
