using System.Threading.Tasks;
using GraphOverflow.Services;
using GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes;
using GraphQL.Types;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.RootGraphTypes
{
  public class QueryType : ObjectGraphType
  {
    #region Members
    private readonly ITagService tagService;
    private readonly IQuestionService questionService;
    #endregion Members

    #region Construction
    public QueryType(ITagService tagService, IQuestionService questionService)
    {
      this.tagService = tagService;
      this.questionService = questionService;
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
      Field<NonNullGraphType<ListGraphType<NonNullGraphType<QuestionType>>>>(
        name: "latestQuestions",
        resolve: ResolveLatestQuestions
      );
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

    private async Task<object> ResolveLatestQuestions(IResolveFieldContext<object> context)
    {
      return await questionService.FindLatestQuestions();
    }
    #endregion Resolvers
  }
}
