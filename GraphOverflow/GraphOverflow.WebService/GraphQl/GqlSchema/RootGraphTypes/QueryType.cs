using GraphOverflow.Services;
using GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes;
using GraphQL.Types;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.RootGraphTypes
{
  public class QueryType : ObjectGraphType
  {
    #region Members
    private readonly ITagService tagService;
    #endregion Members

    #region Construction
    public QueryType(ITagService tagService)
    {
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
      Field<StringGraphType>(name: "hello", resolve: ResolveHello);
      Field<StringGraphType>(name: "name", resolve: ResolveName);
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
    public object ResolveHello(IResolveFieldContext<object> context) => "hello";

    public object ResolveName(IResolveFieldContext<object> context) => "Alex";

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
