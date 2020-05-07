using GraphOverflow.Dtos;
using GraphOverflow.Services;
using GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;
using System;
using System.Threading.Tasks;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.RootGraphTypes
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
    private TagDto ResolveTagAdded(IResolveFieldContext context)
    {
      var tag = context.Source as TagDto;
      return tag;
    }
    #endregion Resolvers

    #region Subscription
    private IObservable<TagDto> Subscribe(IResolveFieldContext context)
    {
      //var messageContext = context.UserContext.As<MessageHandlingContext>();
      //var user = messageContext.Get<ClaimsPrincipal>("user");

      //string sub = "Anonymous";
      //if (user != null)
      //  sub = user.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

      //return _chat.Messages(sub);
      var result = tagService.Tags();
      return result;
    }

    private Task<IObservable<TagDto>> SubscribeAsync(IResolveEventStreamContext arg)
    {
      return tagService.TaskAsync();
    }
    #endregion Subscription
  }
}
