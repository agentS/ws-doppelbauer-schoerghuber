using GraphOverflow.Dtos;
using GraphOverflow.GraphQl.OutputGraphTypes;
using GraphOverflow.Services;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;
using System;

namespace GraphOverflow.GraphQl.RootGraphTypes
{
  /// <summary>
  /// reference implementation:
  /// https://github.com/graphql-dotnet/graphql-dotnet/blob/master/src/GraphQL.Tests/Subscription/SubscriptionSchema.cs
  /// </summary>
  public class SubscriptionType : ObjectGraphType
  {
    #region Members
    private readonly ITagService tagService;
    #endregion Members

    #region Construction
    public SubscriptionType(ITagService tagService)
    {
      this.tagService = tagService;
      InitializeTypeName();
      InitializeFields();
    }

    private void InitializeTypeName()
    {
      Name = "Subscription";
    }

    private void InitializeFields()
    {
      AddField(new EventStreamFieldType
      {
        Name = "tagAdded",
        Type = typeof(TagType),
        Resolver = new FuncFieldResolver<TagDto>(ResolveTagAdded),
        Subscriber = new EventStreamResolver<TagDto>(Subscribe)
      });
    }
    #endregion Construction

    #region Resolvers
    private TagDto ResolveTagAdded(ResolveFieldContext context)
    {
      var tag = context.Source as TagDto;
      return tag;
    }
    #endregion Resolvers

    #region Subscription
    private IObservable<TagDto> Subscribe(ResolveEventStreamContext arg)
    {
      return tagService.Tags();
    }
    #endregion Subscription
  }
}
