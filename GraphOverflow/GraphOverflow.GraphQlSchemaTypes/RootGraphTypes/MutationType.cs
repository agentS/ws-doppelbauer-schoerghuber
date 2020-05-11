using GraphOverflow.GraphQl.OutputGraphTypes;
using GraphOverflow.Services;
using GraphQL.Types;

namespace GraphOverflow.GraphQl.RootGraphTypes
{
  public class MutationType : ObjectGraphType
  {
    #region Members
    private readonly ITagService tagService;
    #endregion Members

    #region Construction
    public MutationType(ITagService tagService)
    {
      this.tagService = tagService;
      InitializeTypeName();
      InitializeFields();
    }

    private void InitializeTypeName()
    {
      Name = "Mutation";
    }

    private void InitializeFields()
    {
      Field<TagType>(
        name: "addTag",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<StringGraphType>>{ Name = "tagName" }),
        resolve: ResolveAddTag
      ).Description = "adds a graphoverflow tag";
    }
    #endregion Construction

    #region Resolvers
    public object ResolveAddTag(ResolveFieldContext<object> context)
    {
      var tagName = context.GetArgument<string>("tagName");
      var createdTag = tagService.AddTag(tagName);
      return createdTag;
    }
    
    
    #endregion Resolvers
  }
}
