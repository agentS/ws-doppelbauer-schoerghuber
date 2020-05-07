using GraphOverflow.GraphQl.OutputGraphTypes;
using GraphOverflow.Services;
using GraphQL.Types;

namespace GraphOverflow.GraphQl.RootGraphTypes
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
      Field<NonNullGraphType<ListGraphType<NonNullGraphType<TagType>>>>(name: "allTags", resolve: ResolveAllTags);
    }
    #endregion Construction

    #region Resolvers
    public object ResolveHello(ResolveFieldContext<object> context)
    {
      return "hello";
    }

    public object ResolveName(ResolveFieldContext<object> context)
    {
      return "Alex";
    }

    public object ResolveAllTags(ResolveFieldContext<object> context)
    {
      return tagService.FindAllTags();
    }
    #endregion Resolvers
  }
}
