using GraphOverflow.Services;
using GraphOverflow.WebService.Constants;
using GraphOverflow.WebService.GraphQl.Extensions;
using GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes;
using GraphQL.Types;
using System.Threading.Tasks;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.RootGraphTypes
{
  public class QueryType : ObjectGraphType
  {
    #region Members
    private readonly ITagService tagService;
    private readonly IUserService userService;
    #endregion Members

    #region Construction
    public QueryType(ITagService tagService, IUserService userService)
    {
      this.userService = userService;
      this.tagService = tagService;
      InitializeTypeName();
      InitializeFields();
    }

    private void InitializeTypeName()
    {
      Name = "Query";
    }

    private void InitializeFields()
    {
      var meField = Field<UserGraphType>(name: "me", resolve: ResolveUser);
      meField.RequirePermission(UserPermissionConstants.USER_PERMISSION);

      Field<NonNullGraphType<ListGraphType<NonNullGraphType<TagType>>>>(
        name: "allTags", resolve: ResolveAllTags
      );

      Field<NonNullGraphType<ListGraphType<NonNullGraphType<TagType>>>>(
        name: "tags",
        resolve: ResolveTags,
        arguments: new QueryArguments(new QueryArgument<StringGraphType>() 
          { 
            Name = "tagName",
            DefaultValue = string.Empty 
          }
        )
      ).Description = "get all tags that match the %tagName%";
    }
    #endregion Construction

    #region Resolvers

    public async Task<object> ResolveUser(IResolveFieldContext<object> context)
    {
      GraphQlUserContext userContext = context.UserContext as GraphQlUserContext;
      var userId = userContext.User.Id;
      return await userService.FindUserById(userId);
    }

    public object ResolveAllTags(IResolveFieldContext<object> context)
    {
      return tagService.FindAllTags();
    }

    private object ResolveTags(IResolveFieldContext<object> context)
    {
      var tagName = context.Arguments["tagName"] as string;
      return tagService.FindAllTagsByName(tagName);
    }
    #endregion Resolvers
  }
}
